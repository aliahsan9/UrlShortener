using FluentValidation;

namespace UrlShortener.Application.Features.UrlMappings.CreateShortUrl;

public sealed class CreateShortUrlRequestValidator
    : AbstractValidator<CreateShortUrlRequest>
{
    private const int MaxUrlLength = 2048;

    public CreateShortUrlRequestValidator()
    {
        RuleFor(request => request.OriginalUrl)
            .NotEmpty()
            .WithMessage("Original URL is required.")
            .MaximumLength(MaxUrlLength)
            .WithMessage($"Original URL cannot exceed {MaxUrlLength} characters.")
            .Must(BeValidHttpUrl)
            .WithMessage("Original URL must be a valid HTTP or HTTPS URL.");
    }

    private static bool BeValidHttpUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            return false;
        }

        if (!Uri.TryCreate(
                url,
                UriKind.Absolute,
                out var uri))
        {
            return false;
        }

        return uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps;
    }
}
