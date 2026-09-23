using Microsoft.EntityFrameworkCore;
using ShortUrl.Application.Common.Interfaces;
using ShortUrl.Domain.Entities;

namespace ShortUrl.Infrastructure.Persistence.Repositories;

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
        return await _context.UrlMappings
            .AsNoTracking()
            .AnyAsync(
                x => x.ShortCode == shortCode,
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
        return await _context.UrlMappings
            .FirstOrDefaultAsync(
                x => x.ShortCode == shortCode,
                cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
