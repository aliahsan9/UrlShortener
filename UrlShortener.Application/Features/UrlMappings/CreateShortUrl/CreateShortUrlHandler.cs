using MediatR;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.ValueObjects;

namespace UrlShortener.Application.Features.UrlMappings.CreateShortUrl;

public sealed class CreateShortUrlHandler
    : IRequestHandler<CreateShortUrlCommand, CreateShortUrlResponse>
{
    private const int MaxGenerationAttempts = 5;

    private readonly IUrlMappingRepository _repository;
    private readonly IShortCodeGenerator _shortCodeGenerator;

    public CreateShortUrlHandler(
        IUrlMappingRepository repository,
        IShortCodeGenerator shortCodeGenerator)
    {
        _repository = repository;
        _shortCodeGenerator = shortCodeGenerator;
    }

    public async Task<CreateShortUrlResponse> Handle(
        CreateShortUrlCommand request,
        CancellationToken cancellationToken)
    {
        for (int attempt = 1; attempt <= MaxGenerationAttempts; attempt++)
        {
            var shortCode = _shortCodeGenerator.Generate();

            var exists = await _repository.ExistsByShortCodeAsync(
                shortCode,
                cancellationToken);

            if (exists)
            {
                continue;
            }

            var urlMapping = UrlMapping.Create(
                OriginalUrl.Create(request.OriginalUrl),
                ShortCode.Create(shortCode));

            await _repository.AddAsync(
                urlMapping,
                cancellationToken);

            await _repository.SaveChangesAsync(
                cancellationToken);

            return new CreateShortUrlResponse(
                urlMapping.Id,
                urlMapping.OriginalUrl.Value,
                urlMapping.ShortCode.Value);
        }

        throw new InvalidOperationException(
            "Unable to generate a unique short code after multiple attempts.");
    }
}
