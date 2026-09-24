using MediatR;
namespace UrlShortener.Application.Features.UrlMappings.GetUrlMapping;

public sealed record GetUrlMappingQuery(
    string ShortCode
) : IRequest<GetUrlMappingResponse?>;
