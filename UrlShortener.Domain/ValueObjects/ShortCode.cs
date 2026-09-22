namespace UrlShortener.Domain.ValueObjects;

public sealed class ShortCode
{
    private const int RequiredLength = 6;

    private const string AllowedCharacters =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
        "abcdefghijklmnopqrstuvwxyz" +
        "0123456789";

    public string Value { get; }

    private ShortCode(string value)
    {
        Value = value;
    }

    public static ShortCode Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(
                "Short code cannot be empty.",
                nameof(value));
        }

        if (value.Length != RequiredLength)
        {
            throw new ArgumentException(
                $"Short code must be exactly {RequiredLength} characters.",
                nameof(value));
        }

        if (value.Any(character => !AllowedCharacters.Contains(character)))
        {
            throw new ArgumentException(
                "Short code can contain only letters and digits.",
                nameof(value));
        }

        return new ShortCode(value);
    }

    public override string ToString()
    {
        return Value;
    }
}
