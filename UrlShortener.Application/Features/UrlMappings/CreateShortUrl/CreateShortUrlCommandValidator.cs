using FluentValidation;

namespace ShortUrl.Application.Features.UrlMappings.CreateShortUrl;

public sealed class CreateShortUrlCommandValidator
    : AbstractValidator<CreateShortUrlCommand>
{
    public CreateShortUrlCommandValidator()
    {
        RuleFor(x => x.OriginalUrl)
            .NotEmpty()
            .MaximumLength(2048)
            .Must(BeValidUrl)
            .WithMessage("A valid absolute URL is required.");
    }

    private static bool BeValidUrl(string value)
    {
        return Uri.TryCreate(
            value,
            UriKind.Absolute,
            out var uri)
            && (uri.Scheme == Uri.UriSchemeHttp
                || uri.Scheme == Uri.UriSchemeHttps);
    }
}
