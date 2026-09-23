using MediatR;

namespace ShortUrl.Application.Features.UrlMappings.CreateShortUrl;

public sealed record CreateShortUrlCommand(
    string OriginalUrl
) : IRequest<CreateShortUrlResponse>;
