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

    [Fact]
    public void Create_WithPastExpiration_ShouldThrowException()
    {
        // Arrange
        var originalUrl =
            OriginalUrl.Create("https://example.com");

        var shortCode =
            ShortCode.Create("aB72x9");

        var expiration =
            DateTime.UtcNow.AddMinutes(-10);

        // Act
        var action = () =>
            UrlMapping.Create(
                originalUrl,
                shortCode,
                expiration);

        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Create_WithFutureExpiration_ShouldCreateMapping()
    {
        // Arrange
        var originalUrl =
            OriginalUrl.Create("https://example.com");

        var shortCode =
            ShortCode.Create("aB72x9");

        var expiration =
            DateTime.UtcNow.AddDays(7);

        // Act
        var mapping =
            UrlMapping.Create(
                originalUrl,
                shortCode,
                expiration);

        // Assert
        Assert.Equal(expiration, mapping.ExpiresAtUtc);
    }

    [Fact]
    public void IsExpired_WhenExpirationHasPassed_ShouldReturnTrue()
    {
        // Arrange
        var originalUrl =
            OriginalUrl.Create("https://example.com");

        var shortCode =
            ShortCode.Create("aB72x9");

        var expiration =
            DateTime.UtcNow.AddMinutes(10);

        var mapping =
            UrlMapping.Create(
                originalUrl,
                shortCode,
                expiration);

        var futureTime =
            DateTime.UtcNow.AddMinutes(20);

        // Act
        var result =
            mapping.IsExpired(futureTime);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsExpired_WhenExpirationHasNotPassed_ShouldReturnFalse()
    {
        // Arrange
        var originalUrl =
            OriginalUrl.Create("https://example.com");

        var shortCode =
            ShortCode.Create("aB72x9");

        var expiration =
            DateTime.UtcNow.AddMinutes(20);

        var mapping =
            UrlMapping.Create(
                originalUrl,
                shortCode,
                expiration);

        var currentTime =
            DateTime.UtcNow;

        // Act
        var result =
            mapping.IsExpired(currentTime);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Deactivate_ShouldSetIsActiveToFalse()
    {
        // Arrange
        var originalUrl =
            OriginalUrl.Create("https://example.com");

        var shortCode =
            ShortCode.Create("aB72x9");

        var mapping =
            UrlMapping.Create(
                originalUrl,
                shortCode);

        // Act
        mapping.Deactivate();

        // Assert
        Assert.False(mapping.IsActive);
    }
}
