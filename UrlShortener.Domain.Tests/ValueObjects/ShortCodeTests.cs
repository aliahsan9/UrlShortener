using UrlShortener.Domain.ValueObjects;

namespace UrlShortener.Domain.Tests.ValueObjects;

public class ShortCodeTests
{
    [Fact]
    public void Create_WithValidValue_ShouldCreateShortCode()
    {
        // Arrange
        var value = "aB72x9";

        // Act
        var shortCode = ShortCode.Create(value);

        // Assert
        Assert.Equal(value, shortCode.Value);
    }
}
