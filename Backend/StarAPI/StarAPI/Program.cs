using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using Microsoft.OpenApi.Models;
using System.Reflection;
using StarAPI.Context;
using StarAPI.Logic.Game;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<StarDeckContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StarDeckDb")));
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>{});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        buildr =>
        {
            buildr.WithOrigins("http://localhost:4200", "http://localhost:7023")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(buildr =>
{
    buildr
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
