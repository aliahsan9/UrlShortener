namespace ShortUrl.Domain.Entities;

public sealed class UrlMapping
{
    private UrlMapping()
    {
    }

    public Guid Id { get; private set; }

    public string OriginalUrl { get; private set; } = null!;

    public string ShortCode { get; private set; } = null!;

    public DateTime CreatedAtUtc { get; private set; }

    public static UrlMapping Create(
        string originalUrl,
        string shortCode)
    {
        if (string.IsNullOrWhiteSpace(originalUrl))
            throw new ArgumentException(
                "Original URL cannot be empty.",
                nameof(originalUrl));

        if (string.IsNullOrWhiteSpace(shortCode))
            throw new ArgumentException(
                "Short code cannot be empty.",
                nameof(shortCode));

        return new UrlMapping
        {
            Id = Guid.NewGuid(),
            OriginalUrl = originalUrl,
            ShortCode = shortCode,
            CreatedAtUtc = DateTime.UtcNow
        };
    }
}
