using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CyberSecurityBot.Core.Models;

namespace CyberSecurityBot.Core.Services
{
    public class KeywordMatcher
    {
        private readonly Random _rng;

        public KeywordMatcher(Random rng = null)
        {
            _rng = rng ?? new Random();
        }

        public Response FindBestMatch(string input, IReadOnlyList<Response> responses)
        {
            if (string.IsNullOrWhiteSpace(input) || responses == null) return null;

            var scored = new List<(Response resp, int score)>();
            foreach (var r in responses)
            {
                int score = CountMatches(input, r.keywords);
                if (score > 0) scored.Add((r, score));
            }

            if (scored.Count == 0) return null;

            int maxScore = scored.Max(s => s.score);
            var top = scored.Where(s => s.score == maxScore).Select(s => s.resp).ToList();
            return top.Count == 1 ? top[0] : top[_rng.Next(top.Count)];
        }

        public int CountMatches(string input, string[] keywords)
        {
            if (string.IsNullOrWhiteSpace(input) || keywords == null || keywords.Length == 0) return 0;

            int matches = 0;
            string lowerInput = input.ToLowerInvariant();

            foreach (var kw in keywords)
            {
                if (string.IsNullOrWhiteSpace(kw)) continue;
                string lowerKw = kw.ToLowerInvariant().Trim();

                if (lowerKw.Contains(" "))
                {
                    if (lowerInput.Contains(lowerKw)) matches++;
                }
                else if (Regex.IsMatch(lowerInput, $@"\b{Regex.Escape(lowerKw)}\b"))
                {
                    matches++;
                }
                else if (lowerInput.Contains(lowerKw))
                {
                    matches++;
                }
            }

            return matches;
        }
    }
}
