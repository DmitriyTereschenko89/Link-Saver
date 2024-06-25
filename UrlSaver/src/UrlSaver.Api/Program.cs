using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using UrlSaver.Api.Middleware;
using UrlSaver.Api.Profiles;
using UrlSaver.Data.Identity;
using UrlSaver.Domain.Common;
using UrlSaver.Domain.Entities;
using UrlSaver.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
var cacheOptions = builder.Configuration.GetSection("CacheSettings");

builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole(options =>
{
    options.UseUtcTimestamp = true;
    options.TimestampFormat = "yyyy-MM-dd HH:mm:ss";
    options.JsonWriterOptions = new JsonWriterOptions
    {
        Indented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerDocument(options =>
{
    options.PostProcess = document =>
    {
        document.Info = new NSwag.OpenApiInfo
        {
            Title = "Url Shortener Api.",
            Version = "v1",
            Description = "An ASP.NET Core Web API for generation short url from long url."
        };
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy", builder =>
    {
        _ = builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddAutoMapper(typeof(UrlProfile));
builder.Services.Configure<EncodeOptions>(builder.Configuration.GetSection("EncodeSettings"));
builder.Services.Configure<UrlLifespanOptions>(builder.Configuration.GetSection("UrlLifespanSettings"));
builder.Services.Configure<CacheOptions>(cacheOptions);
builder.Services.AddMemoryCache(options =>
{
    _ = new MemoryCacheOptions
    {
        SizeLimit = cacheOptions.Get<CacheOptions>().SizeLimit
    };
});
builder.Services.AddScoped<IUrlRepository, UrlRepository>();
builder.Services.AddScoped<IUrlService, UrlService>();
builder.Services.AddScoped<IEncodeService, EncodeService>();
builder.Services.AddScoped<IUrlGeneratorService, UrlGeneratorService>();
builder.Services.AddDbContext<UrlDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Npqsql")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("Policy");
app.UseOpenApi();
app.UseOpenApi();
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
