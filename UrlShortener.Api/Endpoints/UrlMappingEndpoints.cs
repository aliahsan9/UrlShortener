using MediatR;
using FluentValidation;
using UrlShortener.Application.Features.UrlMappings.CreateShortUrl;
using UrlShortener.Application.Features.UrlMappings.GetUrlMapping;

namespace UrlShortener.API.Endpoints;

public static class UrlMappingEndpoints
{
    public static IEndpointRouteBuilder MapUrlMappingEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/urls")
            .WithTags("URL Shortener");

        group.MapGet(
            "/{shortCode}",
            async (
                string shortCode,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new GetUrlMappingQuery(shortCode),
                    cancellationToken);

                return result is null
                    ? Results.NotFound()
                    : Results.Redirect(result.OriginalUrl);
            })
            .WithName("RedirectShortUrl");
        group.MapPost(
            "/",
            async (
                CreateShortUrlRequest request,
                ISender sender,
                IValidator<CreateShortUrlRequest> validator,
                CancellationToken cancellationToken) =>
            {
                var validation = await validator.ValidateAsync(
                    request,
                    cancellationToken);
                if (!validation.IsValid)
                {
                    return Results.ValidationProblem(
                        validation.ToDictionary());
                }

                var command = new CreateShortUrlCommand(
                    request.OriginalUrl);

                var result = await sender.Send(
                    command,
                    cancellationToken);

                return Results.Created(
                    $"/api/urls/{result.ShortCode}",
                    result);
            })
            .WithName("CreateShortUrl");

        return app;
    }
}
