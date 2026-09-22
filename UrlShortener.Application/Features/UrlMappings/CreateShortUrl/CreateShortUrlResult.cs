namespace UrlShortener.Application.Features.UrlMappings.CreateShortUrl;

public sealed record CreateShortUrlResult(
    string ShortCode,
    string ShortUrl);
