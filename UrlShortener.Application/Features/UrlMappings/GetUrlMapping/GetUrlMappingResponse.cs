namespace ShortUrl.Application.Features.UrlMappings.GetUrlMapping;

public sealed record GetUrlMappingResponse(
    Guid Id,
    string OriginalUrl,
    string ShortCode);
