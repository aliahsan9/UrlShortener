using MediatR;
using ShortUrl.Application.Common.Interfaces;

namespace ShortUrl.Application.Features.UrlMappings.GetUrlMapping;

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

        if (mapping is null)
            return null;

        return new GetUrlMappingResponse(
            mapping.Id,
            mapping.OriginalUrl,
            mapping.ShortCode);
    }
}
