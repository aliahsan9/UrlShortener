using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Common.Interfaces;  
public interface IUrlMappingRepository
{
    Task<bool> ExistsByShortCodeAsync( 
        string shortCode,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        UrlMapping urlMapping,
        CancellationToken cancellationToken = default);

    Task<UrlMapping?> GetByShortCodeAsync(
        string shortCode,
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}
