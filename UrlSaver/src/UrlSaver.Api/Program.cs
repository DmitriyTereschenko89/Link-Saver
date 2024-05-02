using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using UrlSaver.Domain.Common;
using UrlSaver.Infrastructure.Identity;
using UrlSaver.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddScoped<IUrlRepository, UrlRepository>();
builder.Services.AddScoped<IEncodeService, EncodeService>();
builder.Services.AddScoped<IUrlGeneratorService, UrlGeneratorService>();
builder.Services.AddDbContext<UrlDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSqlServer")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("api/{url}", (string url, [FromServices] IEncodeService encodeService) => encodeService.Encode(url));

//app.MapPost();

app.Run();