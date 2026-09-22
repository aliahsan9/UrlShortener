using UrlShortener.Domain.Entities;
using UrlShortener.Domain.ValueObjects;

namespace UrlShortener.Domain.Tests.Entities;

public class UrlMappingTests
{
    [Fact]
    public void Create_WithValidData_ShouldCreateMapping()
    {
        // Arrange
        var originalUrl =
            OriginalUrl.Create("https://example.com");

        var shortCode =
            ShortCode.Create("aB72x9");

        // Act
        var mapping =
            UrlMapping.Create(originalUrl, shortCode);

        // Assert
        Assert.NotEqual(Guid.Empty, mapping.Id);
        Assert.Equal(originalUrl, mapping.OriginalUrl);
        Assert.Equal(shortCode, mapping.ShortCode);
        Assert.True(mapping.IsActive);
        Assert.Null(mapping.ExpiresAtUtc);
    }
}
