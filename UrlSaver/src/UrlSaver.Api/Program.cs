using Microsoft.EntityFrameworkCore;
using UrlSaver.Domain.Common;
using UrlSaver.Infrastructure.Identity;
using UrlSaver.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IUrlRepository, UrlRepository>();
builder.Services.AddTransient<IEncodeService, EncodeService>();
builder.Services.AddTransient<IUrlGeneratorService, UrlGeneratorService>();
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

app.MapGet("api/{url}", (string url) => url);

//app.MapPost();

app.Run();