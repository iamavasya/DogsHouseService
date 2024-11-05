using DogsHouseService.Infrastructure.Interfaces;
using DogsHouseService.Infrastructure.Repositories;
using DogsHouseService.BusinessLogic.Interfaces;
using DogsHouseService.BusinessLogic.Services;
using DogsHouseService.Infrastructure.DbContextes;
using Microsoft.EntityFrameworkCore;
using DogsHouseService.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DogsDbContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDogsRepository, DogsRepository>();
builder.Services.AddScoped<IDogsService, DogService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware: 10 requests per second
app.UseMiddleware<RateLimitingMiddleware>(10, TimeSpan.FromSeconds(1));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
