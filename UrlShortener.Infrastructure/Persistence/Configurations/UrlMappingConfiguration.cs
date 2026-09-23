using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShortUrl.Domain.Entities;

namespace ShortUrl.Infrastructure.Persistence.Configurations;

public sealed class UrlMappingConfiguration
    : IEntityTypeConfiguration<UrlMapping>
{
    public void Configure(
        EntityTypeBuilder<UrlMapping> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.OriginalUrl)
            .IsRequired()
            .HasMaxLength(2048);

        builder.Property(x => x.ShortCode)
            .IsRequired()
            .HasMaxLength(7);

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.HasIndex(x => x.ShortCode)
            .IsUnique();
    }
}
