using UrlShortener.Domain.ValueObjects;

namespace UrlShortener.Domain.Tests.ValueObjects;

public class OriginalUrlTests
{
    [Fact]
    public void Create_WithValidHttpsUrl_ShouldCreateOriginalUrl()
    {
        // Arrange
        var value = "https://example.com/products";

        // Act
        var originalUrl = OriginalUrl.Create(value);

        // Assert
        Assert.Equal(value, originalUrl.Value);
    }

    [Fact]
    public void Create_WithValidHttpUrl_ShouldCreateOriginalUrl()
    {
        // Arrange
        var value = "http://example.com";

        // Act
        var originalUrl = OriginalUrl.Create(value);

        // Assert
        Assert.Equal(value, originalUrl.Value);
    }

    [Fact]
    public void Create_WithEmptyValue_ShouldThrowException()
    {
        // Arrange
        var value = "";

        // Act
        var action = () => OriginalUrl.Create(value);

        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Create_WithInvalidUrl_ShouldThrowException()
    {
        // Arrange
        var value = "not-a-url";

        // Act
        var action = () => OriginalUrl.Create(value);

        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Create_WithUnsupportedScheme_ShouldThrowException()
    {
        // Arrange
        var value = "ftp://example.com";

        // Act
        var action = () => OriginalUrl.Create(value);

        // Assert
        Assert.Throws<ArgumentException>(action);
    }
}
