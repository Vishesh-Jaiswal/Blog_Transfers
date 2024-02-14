using FlightApp.Contexts;
using FlightApp.Interfaces;
using FlightApp.Models;
using FlightApp.Repositories;
using FlightApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FlightAppDBContext>(opts=>
    {
        opts.UseSqlServer(builder.Configuration.GetConnectionString("FlightAppasp"));
});
builder.Services.AddScoped<IRepository<string,User>, UserRepository>();
builder.Services.AddScoped<IRepository<int, Flight>, FlightRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFlightService, FlightService>();

var app = builder.Build();

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
