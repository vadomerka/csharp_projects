﻿using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using OrdersService.Infrastructure;
using OrdersService.Infrastructure.Notifications;
using OrdersService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Host=localhost;Database=orders_db;Username=postgres;Password=postgres";
builder.Services.AddDbContext<OrderDBContext>(options => {
    options.UseNpgsql(connectionString);
    options.EnableSensitiveDataLogging(false);
    options.LogTo(_ => { });
});


// Добавляем Consumer для получения сообщений о статусе платежа
builder.Services.AddSingleton(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var config = new ConsumerConfig
    {
        BootstrapServers = configuration.GetSection("Kafka:BootstrapServers").Value,
        GroupId = configuration.GetSection("Kafka:GroupId").Value,
        EnableAutoCommit = false,

        SocketTimeoutMs = 60000,
        SessionTimeoutMs = 30000,
        MaxPollIntervalMs = 300000,
        ReconnectBackoffMs = 1000,
        ReconnectBackoffMaxMs = 10000
    };
    return new ConsumerBuilder<string, string>(config).Build();
});

builder.Services.AddHostedService<PaymentStatusProcessor>();

// Добавляем сервис для обработки уведомлений о статусе платежа
builder.Services.AddHostedService(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new PaymentStatusConsumer(
        sp.GetRequiredService<IServiceScopeFactory>(),
        sp.GetRequiredService<ILogger<PaymentStatusConsumer>>(),
        sp.GetRequiredService<IConsumer<string, string>>(),
        configuration.GetSection("Kafka:PaymentStatusTopic").Value
            ?? throw new InvalidOperationException("Kafka payment status topic is not configured")
    );
});


builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var producerConfig = new ProducerConfig
    {
        BootstrapServers = config.GetSection("Kafka:BootstrapServers").Value
    };
    return new ProducerBuilder<string, string>(producerConfig).Build();
});

builder.Services.AddHostedService(sp =>
{
    var producer = sp.GetRequiredService<IProducer<string, string>>();
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new NotificationSender(
        sp.GetRequiredService<IServiceScopeFactory>(),
        sp.GetRequiredService<ILogger<NotificationSender>>(),
        producer,
        configuration.GetSection("Kafka:Topic").Value
            ?? throw new InvalidOperationException("Kafka topic is not configured")
    );
});

builder.Services.AddScoped<OrderRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderDBContext>();
    //dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
    dbContext.SaveChanges();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
