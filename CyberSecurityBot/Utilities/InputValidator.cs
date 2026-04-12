using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CyberSecurityBot.Utilities
{
    public static class InputValidator
    {
        public static bool IsNullOrEmpty(string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        // Normalize and split input into tokens (lowercase, remove punctuation)
        public static string[] Tokenize(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return Array.Empty<string>();

            // remove punctuation (keep apostrophes if you want), lower-case
            string normalized = Regex.Replace(input.ToLowerInvariant(), @"[^\w\s]", " ");
            var tokens = normalized.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return tokens;
        }

        // Count how many keywords match the input. Supports multi-word keywords.
        public static int CountKeywordMatches(string input, string[] keywords)
        {
            if (string.IsNullOrWhiteSpace(input) || keywords == null || keywords.Length == 0)
                return 0;

            int matches = 0;
            string lowerInput = input.ToLowerInvariant();

            foreach (var kw in keywords)
            {
                if (string.IsNullOrWhiteSpace(kw)) continue;

                string lowerKw = kw.ToLowerInvariant().Trim();

                // If keyword is multi-word, check contains (phrase match)
                if (lowerKw.Contains(" "))
                {
                    if (lowerInput.Contains(lowerKw))
                        matches++;
                }
                else
                {
                    // For single-word keywords, match whole words only using regex word boundary
                    if (Regex.IsMatch(lowerInput, $@"\b{Regex.Escape(lowerKw)}\b"))
                        matches++;
                    else
                    {
                        // fallback: partial match (e.g., "safe" matches "safety") - optional
                        if (lowerInput.Contains(lowerKw))
                            matches++;
                    }
                }
            }

            return matches;
        }
    }
}