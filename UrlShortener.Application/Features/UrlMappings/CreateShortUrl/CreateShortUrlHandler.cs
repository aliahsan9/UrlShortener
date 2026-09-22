using UrlShortener.Application.Abstractions.Repositories;
using UrlShortener.Application.Abstractions.Services;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Features.UrlMappings.CreateShortUrl;

public sealed class CreateShortUrlHandler
{
    private readonly IUrlMappingRepository _urlMappingRepository;
    private readonly IShortCodeGenerator _shortCodeGenerator;

    public CreateShortUrlHandler(
        IUrlMappingRepository urlMappingRepository,
        IShortCodeGenerator shortCodeGenerator)
    {
        _urlMappingRepository = urlMappingRepository;
        _shortCodeGenerator = shortCodeGenerator;
    }

    public async Task<CreateShortUrlResult> HandleAsync(
        CreateShortUrlRequest request,
        CancellationToken cancellationToken)
    {
        var shortCode = _shortCodeGenerator.Generate();

        while (await _urlMappingRepository.ExistsByShortCodeAsync(
            shortCode,
            cancellationToken))
        {
            shortCode = _shortCodeGenerator.Generate();
        }

        var urlMapping = UrlMapping.Create(
            shortCode,
            request.OriginalUrl);

        await _urlMappingRepository.AddAsync(
            urlMapping,
            cancellationToken);

        return new CreateShortUrlResult(
            urlMapping.ShortCode,
            $"https://short.ly/{urlMapping.ShortCode}");
    }
}
