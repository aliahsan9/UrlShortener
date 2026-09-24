using MediatR;

namespace UrlShortener.Application.Features.UrlMappings.CreateShortUrl;

public sealed record CreateShortUrlCommand(
    string OriginalUrl
) : IRequest<CreateShortUrlResponse>;
