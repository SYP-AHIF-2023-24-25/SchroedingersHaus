namespace Core.Helper;

public class RandomStringGenerate
{
    private static readonly string Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private static readonly string Lower = Upper.ToLower();
    private static readonly string Digits = "0123456789";
    private static readonly string Alphanum = Upper + Lower + Digits;

    private readonly Random _random = new Random();
    private readonly char[] _symbols = Alphanum.ToCharArray();

    /// <summary>
    /// Generates a random alphanumeric string of the specified length.
    /// </summary>
    /// <param name="length">The length of the string to generate.</param>
    /// <returns>A random alphanumeric string.</returns>
    public string GenerateRandomString(int length)
    {
        var buffer = new char[length];

        for (int idx = 0; idx < buffer.Length; ++idx)
        {
            buffer[idx] = _symbols[_random.Next(_symbols.Length)];
        }

        return new string(buffer);
    }
}