using Microsoft.EntityFrameworkCore;
using FluentValidation;
using UrlShortener.API.Endpoints;
using UrlShortener.Application.Features.UrlMappings.CreateShortUrl;
using UrlShortener.Infrastructure;
using UrlShortener.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Avoid the Windows Event Log provider, which can fail when the process has no
// permission to write to the machine event log.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMediatR(configuration =>
    configuration.RegisterServicesFromAssembly(typeof(CreateShortUrlCommand).Assembly));
builder.Services.AddScoped<IValidator<CreateShortUrlRequest>, CreateShortUrlRequestValidator>();
builder.Services.AddInfrastructure(
    builder.Configuration.GetConnectionString("ShortUrlDatabase") ?? "Data Source=shorturls.db");
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var database = scope.ServiceProvider.GetRequiredService<ShortUrlDbContext>();
    await database.Database.EnsureCreatedAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapUrlMappingEndpoints();

app.Run();
