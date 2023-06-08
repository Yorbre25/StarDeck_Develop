using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using Microsoft.OpenApi.Models;
using System.Reflection;
using StarAPI.Context;
using StarAPI.Logic.Game;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder();
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
});



// Add services to the container.
// builder.Services.AddDbContext<StarDeckContext>(options => options.UseSqlite("Data source = Context/StarDeck.db"));
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
