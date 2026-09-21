using UrlShortener.Domain.ValueObjects;

namespace UrlShortener.Domain.Entities;

public sealed class UrlMapping
{
    public Guid Id { get; private set; }

    public OriginalUrl OriginalUrl { get; private set; }

    public ShortCode ShortCode { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }

    public DateTime? ExpiresAtUtc { get; private set; }

    public bool IsActive { get; private set; }

    private UrlMapping(
        Guid id,
        OriginalUrl originalUrl,
        ShortCode shortCode,
        DateTime createdAtUtc,
        DateTime? expiresAtUtc)
    {
        Id = id;
        OriginalUrl = originalUrl;
        ShortCode = shortCode;
        CreatedAtUtc = createdAtUtc;
        ExpiresAtUtc = expiresAtUtc;
        IsActive = true;
    }

    public static UrlMapping Create(
        OriginalUrl originalUrl,
        ShortCode shortCode,
        DateTime? expiresAtUtc = null)
    {
        ArgumentNullException.ThrowIfNull(originalUrl);
        ArgumentNullException.ThrowIfNull(shortCode);

        var createdAtUtc = DateTime.UtcNow;

        if (expiresAtUtc.HasValue &&
            expiresAtUtc.Value <= createdAtUtc)
        {
            throw new ArgumentException(
                "Expiration time must be in the future.",
                nameof(expiresAtUtc));
        }

        return new UrlMapping(
            Guid.NewGuid(),
            originalUrl,
            shortCode,
            createdAtUtc,
            expiresAtUtc);
    }

    public bool IsExpired(DateTime utcNow)
    {
        return ExpiresAtUtc.HasValue &&
               ExpiresAtUtc.Value <= utcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
