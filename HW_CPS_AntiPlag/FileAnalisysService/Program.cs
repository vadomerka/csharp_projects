using FilesAnaliseService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Host=localhost;Database=analyse_db;Username=postgres;Password=postgres";
builder.Services.AddDbContext<AnalisysDBContext>(options => options.UseNpgsql(connectionString));

//builder.Services.AddScoped<IAnalisysResultRepository, AnalisysResultRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AnalisysDBContext>();
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();  // База создается один раз при старте приложения
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