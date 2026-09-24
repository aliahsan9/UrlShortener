using MediatR;
using UrlShortener.Application.Common.Interfaces;

namespace UrlShortener.Application.Features.UrlMappings.GetUrlMapping;

public sealed class GetUrlMappingHandler
    : IRequestHandler<
        GetUrlMappingQuery,
        GetUrlMappingResponse?>
{
    private readonly IUrlMappingRepository _repository;

    public GetUrlMappingHandler(
        IUrlMappingRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetUrlMappingResponse?> Handle(
        GetUrlMappingQuery request,
        CancellationToken cancellationToken)
    {
        var mapping = await _repository.GetByShortCodeAsync(
            request.ShortCode,
            cancellationToken);

        if (mapping is null || !mapping.IsActive || mapping.IsExpired(DateTime.UtcNow))
            return null;

        return new GetUrlMappingResponse(
            mapping.Id,
            mapping.OriginalUrl.Value,
            mapping.ShortCode.Value);
    }
}
