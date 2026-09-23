using Microsoft.Extensions.DependencyInjection;
using ShortUrl.Application.Common.Interfaces;
using ShortUrl.Infrastructure.Services;

namespace ShortUrl.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddSingleton<
            IShortCodeGenerator,
            RandomShortCodeGenerator>();

        return services;
    }
}
