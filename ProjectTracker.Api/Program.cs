using AutoMapper;
using AutoMapper.Configuration;  
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProjectTracker.Api.Middleware;
using ProjectTracker.Application.Interfaces;
using ProjectTracker.Application.Mapping;
using ProjectTracker.Application.Services;
using ProjectTracker.Infrastructure.Data;
using ProjectTracker.Infrastructure.Repositories;
using Serilog;
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
    options.AddPolicy("InternalPolicy", policy =>
        policy.WithOrigins("https://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// Dependency Injection for Application and Infrastructure layers
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ProjectService>();

builder.Services.AddAutoMapper(config => config.AddProfile<ProjectProfile>());

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .Select(e => new { Field = e.Key, Error = e.Value!.Errors.First().ErrorMessage })
            .ToList();

        return new BadRequestObjectResult(new { Errors = errors });
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("InternalPolicy");

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
