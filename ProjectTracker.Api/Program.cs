using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProjectTracker.Application.Interfaces;
using ProjectTracker.Application.Services;
using ProjectTracker.Infrastructure.Data;
using ProjectTracker.Infrastructure.Repositories;
using System;

var builder = WebApplication.CreateBuilder(args);

var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(dbConnectionString));

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ProjectTracker API",
        Version = "v1"
    });
});

// Basic CORS setup
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// Dependency Injection for Application and Infrastructure layers
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ProjectService>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors("AllowAll");

//Swagger UI(dev only)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectTracker API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
