namespace UrlShortener.Application.Features.UrlMappings.CreateShortUrl;

public sealed record CreateShortUrlResponse(
    Guid Id,
    string OriginalUrl,
    string ShortCode);
