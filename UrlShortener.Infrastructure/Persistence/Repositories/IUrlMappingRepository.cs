using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.ValueObjects;

namespace UrlShortener.Infrastructure.Persistence.Repositories;

public sealed class UrlMappingRepository : IUrlMappingRepository
{
    private readonly ShortUrlDbContext _context;

    public UrlMappingRepository(ShortUrlDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByShortCodeAsync(
        string shortCode,
        CancellationToken cancellationToken = default)
    {
        if (!ShortCode.TryCreate(shortCode, out var parsedShortCode))
            return false;

        return await _context.UrlMappings
            .AsNoTracking()
            .AnyAsync(
                x => x.ShortCode == parsedShortCode!,
                cancellationToken);
    }

    public async Task AddAsync(
        UrlMapping urlMapping,
        CancellationToken cancellationToken = default)
    {
        await _context.UrlMappings.AddAsync(
            urlMapping,
            cancellationToken);
    }

    public async Task<UrlMapping?> GetByShortCodeAsync(
        string shortCode,
        CancellationToken cancellationToken = default)
    {
        if (!ShortCode.TryCreate(shortCode, out var parsedShortCode))
            return null;

        return await _context.UrlMappings
            .FirstOrDefaultAsync(
                x => x.ShortCode == parsedShortCode!,
                cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
