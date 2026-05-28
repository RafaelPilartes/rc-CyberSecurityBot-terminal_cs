using System;
using System.Text.RegularExpressions;

namespace CyberSecurityBot.Core.Utilities
{
    public static class InputValidator
    {
        public const int MaxLength = 500;

        public static ValidationResult Validate(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return ValidationResult.Empty;
            if (input.Length > MaxLength) return ValidationResult.TooLong;
            return ValidationResult.Ok;
        }

        public static bool IsNullOrEmpty(string input) => string.IsNullOrWhiteSpace(input);

        public static string[] Tokenize(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return Array.Empty<string>();
            string normalized = Regex.Replace(input.ToLowerInvariant(), @"[^\w\s]", " ");
            return normalized.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
