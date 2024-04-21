using linksaver.domain.Services;

using Microsoft.AspNetCore.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IEncodeService, EncodeService>();
builder.Services.AddTransient<ILinkGeneratorService, LinkGeneratorService>();
builder.Services.AddTransient<ILinkService, LinkService>();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
app.MapGet("api/{url}", async (string url, ILinkService linkService) =>
{
    var linkModel = await linkService.GetShortLink(url);
    return linkModel;
}).WithOpenApi();

app.Run();