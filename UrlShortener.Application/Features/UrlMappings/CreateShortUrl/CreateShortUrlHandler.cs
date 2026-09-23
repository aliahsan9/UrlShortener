using MediatR;
using ShortUrl.Application.Common.Interfaces;
using ShortUrl.Domain.Entities;
using UrlShortener.Domain.Entities;

namespace ShortUrl.Application.Features.UrlMappings.CreateShortUrl;

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
                request.OriginalUrl,
                shortCode);

            await _repository.AddAsync(
                urlMapping,
                cancellationToken);

            await _repository.SaveChangesAsync(
                cancellationToken);

            return new CreateShortUrlResponse(
                urlMapping.Id,
                urlMapping.OriginalUrl,
                urlMapping.ShortCode);
        }

        throw new InvalidOperationException(
            "Unable to generate a unique short code after multiple attempts.");
    }
}
