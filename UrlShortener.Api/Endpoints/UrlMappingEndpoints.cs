using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.HttpResults;
using ShortUrl.Application.Features.UrlMappings.CreateShortUrl;

namespace ShortUrl.API.Endpoints;

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

        if (result is null)
            return Results.NotFound();

        return Results.Redirect(
            result.OriginalUrl);
    })
    .WithName("RedirectShortUrl");
        group.MapPost(
            "/",
            async (
                CreateShortUrlRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new CreateShortUrlCommand(
                    request.OriginalUrl);

                var result = await sender.Send(
                    command,
                    cancellationToken);

                return Results.Created(
                    $"/{result.ShortCode}",
                    result);
            })
            .WithName("CreateShortUrl");

        return app;
    }
}

public sealed record CreateShortUrlRequest(
    string OriginalUrl);
