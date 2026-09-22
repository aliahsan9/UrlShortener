using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Abstractions.Repositories;

public interface IUrlMappingRepository
{
    Task AddAsync(
        UrlMapping urlMapping,
        CancellationToken cancellationToken);

    Task<bool> ExistsByShortCodeAsync(
        string shortCode,
        CancellationToken cancellationToken);
}
