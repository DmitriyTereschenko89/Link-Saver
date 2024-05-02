using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using UrlSaver.Data.Identity;
using UrlSaver.Domain.Common;
using UrlSaver.Domain.Entities;
using UrlSaver.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<EncodeOptions>(builder.Configuration.GetSection("EncodeSettings"));
builder.Services.AddScoped<IUrlRepository, UrlRepository>();
builder.Services.AddScoped<IUrlService, UrlService>();
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

app.MapGet("api/{url}", (string url, [FromServices] IEncodeService encode) => encode.Encode(url));

//app.MapPost();

app.Run();