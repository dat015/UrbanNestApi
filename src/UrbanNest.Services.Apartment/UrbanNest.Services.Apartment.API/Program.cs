using Microsoft.EntityFrameworkCore;
using UrbanNest.Services.Apartment.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApartmentDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDbContext<ApartmentDbContext>(options =>
{
    options.UseNpgsql(connectionString)
           .UseSnakeCaseNamingConvention();  
});
builder.Services.AddOpenApi();
builder.Services.AddControllers();// <--- bật Controller


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers(); // <--- ánh xạ route controller

app.Run();
