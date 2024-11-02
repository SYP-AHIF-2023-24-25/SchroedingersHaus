using System.Collections.Concurrent;
using System.Net;
using System.Text.RegularExpressions;

namespace Core.Helper;

public class ProfanityFilter
{
    private static ConcurrentDictionary<string, string[]> Words = new ConcurrentDictionary<string, string[]>();
    private static int LargestWordLength = 0;

    // Static constructor to load configurations when the class is first used
    static ProfanityFilter()
    {
        LoadConfigs();
    }

    public static void LoadConfigs()
    {
        try
        {
            using (var client = new WebClient())
            using (var reader = new StreamReader(client.OpenRead("https://docs.google.com/spreadsheets/d/1BBEJs4k9yZjsZck1msIVp6W6g0XqD8jpjEiTsBJ7eyw/export?format=csv")))
            {
                string line;
                int counter = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    counter++;
                    var content = line.Split(',');

                    if (content.Length == 0) continue;

                    var word = content[0];
                    var ignoreWords = content.Length > 1 ? content[1].Split('_') : Array.Empty<string>();

                    if (word.Length > LargestWordLength)
                    {
                        LargestWordLength = word.Length;
                    }

                    Words.TryAdd(word.Replace(" ", ""), ignoreWords);
                }

                Console.WriteLine($"Loaded {counter} words to filter out.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading configurations: {ex.Message}");
        }
    }

    public static List<string> BadWordsFound(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return new List<string>();
        }

        // Leetspeak ersetzen
        input = input.Replace("1", "i")
                     .Replace("!", "i")
                     .Replace("3", "e")
                     .Replace("4", "a")
                     .Replace("@", "a")
                     .Replace("5", "s")
                     .Replace("7", "t")
                     .Replace("0", "o")
                     .Replace("9", "g");

        var badWords = new List<string>();
        input = Regex.Replace(input.ToLower(), "[^a-zA-Z]", "");

        // Über die Zeichen im Wort iterieren
        for (int start = 0; start < input.Length; start++)
        {
            for (int offset = 1; offset < (input.Length + 1 - start) && offset <= LargestWordLength; offset++)
            {
                var wordToCheck = input.Substring(start, offset);
                if (Words.ContainsKey(wordToCheck))
                {
                    var ignoreWords = Words[wordToCheck];
                    var ignore = ignoreWords.Any(ignoreWord => input.Contains(ignoreWord));

                    if (!ignore)
                    {
                        badWords.Add(wordToCheck);
                    }
                }
            }
        }

        foreach (var word in badWords)
        {
            Console.WriteLine($"{word} qualified as a bad word");
        }

        return badWords;
    }

    public static string FilterText(string input)
    {
        var badWords = BadWordsFound(input);
        if (badWords.Count > 0)
        {
            return "This message was blocked because a bad word was found. If you believe this word should not be blocked, please message support.";
        }
        return input;
    }

    public static void ReloadFilter()
    {
        LoadConfigs();
    }
}