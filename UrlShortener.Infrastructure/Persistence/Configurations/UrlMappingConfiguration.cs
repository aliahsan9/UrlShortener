using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.ValueObjects;

namespace UrlShortener.Infrastructure.Persistence.Configurations;

public sealed class UrlMappingConfiguration
    : IEntityTypeConfiguration<UrlMapping>
{
    public void Configure(
        EntityTypeBuilder<UrlMapping> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.OriginalUrl)
            .HasConversion(url => url.Value, value => OriginalUrl.Create(value))
            .IsRequired()
            .HasMaxLength(2048);

        builder.Property(x => x.ShortCode)
            .HasConversion(code => code.Value, value => ShortCode.Create(value))
            .IsRequired()
            .HasMaxLength(6);

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.ExpiresAtUtc);

        builder.HasIndex(x => x.ShortCode)
            .IsUnique();
    }
}
