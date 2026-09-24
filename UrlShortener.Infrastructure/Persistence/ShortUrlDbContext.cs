using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Infrastructure.Persistence;

public sealed class ShortUrlDbContext : DbContext
{
    public ShortUrlDbContext( 
        DbContextOptions<ShortUrlDbContext> options)
        : base(options)
    {
    }

    public DbSet<UrlMapping> UrlMappings => Set<UrlMapping>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ShortUrlDbContext).Assembly);
    }
}
