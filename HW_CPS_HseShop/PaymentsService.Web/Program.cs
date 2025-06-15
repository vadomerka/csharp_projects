using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PaymentsService.Infrastructure;
using PaymentsService.Infrastructure.Notifications;
using PaymentsService.Infrastructure.Repositories;
using PaymentsService.UseCases.Notifications;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Host=localhost;Database=userfiles_db;Username=postgres;Password=postgres";
builder.Services.AddDbContext<AccountDBContext>(options => {
    options.UseNpgsql(connectionString);
    options.EnableSensitiveDataLogging(false);
    options.LogTo(_ => { });
});

builder.Services.AddHostedService<NotificationProcessor>();

builder.Services.AddSingleton(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var config = new ConsumerConfig
    {
        BootstrapServers = configuration.GetSection("Kafka:BootstrapServers").Value,
        GroupId = configuration.GetSection("Kafka:GroupId").Value, // context.Configuration["Kafka:GroupId"],
        EnableAutoCommit = false,
    };
    return new ConsumerBuilder<string, string>(config).Build();
});

builder.Services.AddHostedService(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new NotificationConsumer(
        sp.GetRequiredService<IServiceScopeFactory>(),
        sp.GetRequiredService<ILogger<NotificationConsumer>>(),
        sp.GetRequiredService<IConsumer<string, string>>(),
        configuration.GetSection("Kafka:Topic").Value
            ?? throw new InvalidOperationException("Kafka topic is not configured")
    );
});

builder.Services.AddScoped<AccountRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AccountDBContext>();
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
    dbContext.SaveChanges();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.LaunchBrowser();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
