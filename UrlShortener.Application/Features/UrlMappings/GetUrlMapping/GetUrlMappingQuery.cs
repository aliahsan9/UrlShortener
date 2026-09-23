using MediatR;

namespace ShortUrl.Application.Features.UrlMappings.GetUrlMapping;

public sealed record GetUrlMappingQuery(
    string ShortCode
) : IRequest<GetUrlMappingResponse?>;
