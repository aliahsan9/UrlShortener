using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Infrastructure.Persistence;
using UrlShortener.Infrastructure.Persistence.Repositories;
using UrlShortener.Infrastructure.Services;


namespace UrlShortener.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<ShortUrlDbContext>(options => options.UseSqlite(connectionString));
        services.AddScoped<IUrlMappingRepository, UrlMappingRepository>();
        services.AddSingleton<
            IShortCodeGenerator,
            RandomShortCodeGenerator>();

        return services;
    }
}
