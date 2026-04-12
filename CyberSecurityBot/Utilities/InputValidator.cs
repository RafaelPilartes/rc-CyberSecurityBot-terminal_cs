using System.Linq;

namespace CyberSecurityBot.Utilities
{
    public static class InputValidator
    {
        public static bool IsNullOrEmpty(string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        public static bool ContainsKeyword(string input, string[] keywords)
        {
            string lowerInput = input.ToLowerInvariant();
            return keywords.Any(k => lowerInput.Contains(k));
        }
    }
}