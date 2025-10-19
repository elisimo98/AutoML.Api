using AutoML.Api.Extensions;
using AutoML.Api.Models;
using AutoML.Api.Validation;
using AutoML.Application.Extensions;
using AutoML.Data.Extensions;
using AutoML.Infrastructure.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Register infrastructure services
builder.Services.AddInfrastructureServices(builder.Configuration);

// Register application services
builder.Services.AddApplicationServices(builder.Configuration);

// Register data services
builder.Services.AddDataServices(builder.Configuration);

// Register FluentValidation validators
builder.Services.AddScoped<IValidator<CreateModelConfigRequest>, CreateModelConfigRequestValidator>();

// Register api services
builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
