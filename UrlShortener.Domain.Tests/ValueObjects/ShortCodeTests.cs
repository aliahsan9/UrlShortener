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

    [Fact]
    public void Create_WithEmptyValue_ShouldThrowException()
    {
        // Arrange
        var value = "";

        // Act
        var action = () => ShortCode.Create(value);

        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Create_WithIncorrectLength_ShouldThrowException()
    {
        // Arrange
        var value = "abc";

        // Act
        var action = () => ShortCode.Create(value);

        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void Create_WithSpecialCharacters_ShouldThrowException()
    {
        // Arrange
        var value = "abc-12";

        // Act
        var action = () => ShortCode.Create(value);

        // Assert
        Assert.Throws<ArgumentException>(action);
    }
    [Fact]
    public void Create_WithBase62Characters_ShouldCreateShortCode()
    {
        // Arrange
        var value = "aZ09x1";

        // Act
        var shortCode = ShortCode.Create(value);

        // Assert
        Assert.Equal(value, shortCode.Value);
    }
    [Fact]
    public void Create_WithNonAsciiCharacter_ShouldThrowException()
    {
        // Arrange
        var value = "abc12é";

        // Act
        var action = () => ShortCode.Create(value);

        // Assert
        Assert.Throws<ArgumentException>(action);

    }
}
