namespace UrlShortener.Domain.Entities;

using UrlShortener.Domain.ValueObjects;

public sealed class UrlMapping
{
    private UrlMapping()
    {
    }

    public Guid Id { get; private set; }

    public OriginalUrl OriginalUrl { get; private set; } = null!;

    public ShortCode ShortCode { get; private set; } = null!;

    public DateTime CreatedAtUtc { get; private set; }

    public DateTime? ExpiresAtUtc { get; private set; }

    public bool IsActive { get; private set; }

    public static UrlMapping Create(
        OriginalUrl originalUrl,
        ShortCode shortCode,
        DateTime? expiresAtUtc = null)
    {
        ArgumentNullException.ThrowIfNull(originalUrl);
        ArgumentNullException.ThrowIfNull(shortCode);
        if (expiresAtUtc is not null && expiresAtUtc <= DateTime.UtcNow)
            throw new ArgumentException("Expiration must be in the future.", nameof(expiresAtUtc));

        return new UrlMapping
        {
            Id = Guid.NewGuid(),
            OriginalUrl = originalUrl,
            ShortCode = shortCode,
            CreatedAtUtc = DateTime.UtcNow,
            ExpiresAtUtc = expiresAtUtc,
            IsActive = true
        };
    }

    public bool IsExpired(DateTime utcNow) => ExpiresAtUtc is not null && ExpiresAtUtc <= utcNow;

    public void Deactivate() => IsActive = false;
}
