using System.Security.Cryptography;
using UrlShortener.Application.Common.Interfaces;

namespace UrlShortener.Infrastructure.Services;

public sealed class RandomShortCodeGenerator : IShortCodeGenerator
{
    private const string Alphabet =
        "abcdefghijklmnopqrstuvwxyz" +
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
        "0123456789";

    private const int CodeLength = 6;

    public string Generate()
    {
        Span<char> code = stackalloc char[CodeLength];

        for (int i = 0; i < CodeLength; i++)
        {
            int index = RandomNumberGenerator.GetInt32(Alphabet.Length);

            code[i] = Alphabet[index];
        }

        return new string(code);
    }
}
