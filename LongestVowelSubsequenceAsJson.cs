using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class LongestVowelSubsequenceSolution
{
    public static string LongestVowelSubsequenceAsJson(List<string> words)
    {
        if (words == null || words.Count == 0)
        {
            return JsonSerializer.Serialize(new List<object>());
        }

        var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };
        var results = new List<object>();

        foreach (string word in words)
        {
            string longestVowelSequence = "";
            string currentSequence = "";

            foreach (char c in word)
            {
                if (vowels.Contains(c))
                {
                    currentSequence += c;
                }
                else
                {
                    if (currentSequence.Length > longestVowelSequence.Length)
                    {
                        longestVowelSequence = currentSequence;
                    }
                    currentSequence = "";
                }
            }

            // Son kontrol - kelime sesli harf ile bitiyorsa
            if (currentSequence.Length > longestVowelSequence.Length)
            {
                longestVowelSequence = currentSequence;
            }

            results.Add(new
            {
                word = word,
                sequence = longestVowelSequence,
                length = longestVowelSequence.Length
            });
        }

        return JsonSerializer.Serialize(results);
    }
}