using UrlSaver.Domain.Common;
using UrlSaver.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IUrlRepositoryService, UrlRepositoryService>();
builder.Services.AddTransient<IEncodeService, EncodeService>();
builder.Services.AddTransient<IUrlGeneratorService, UrlGeneratorService>();


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