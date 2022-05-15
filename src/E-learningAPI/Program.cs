using E_learningAPI.Domain.Data;
using MediatR;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using E_learningAPI.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    var services = builder.Services;
    ConfigurationManager configuration = builder.Configuration;

    services.AddControllers();

    services.AddDataExtension(configuration.GetConnectionString("DefaultConnection"));
    services.AddLibraryExtension();
    services.AddAuthExtension(configuration);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
