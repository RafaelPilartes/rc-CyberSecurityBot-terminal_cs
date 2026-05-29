using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CyberSecurityBot.Core.Services
{
    public class SentimentDetector
    {
        private readonly Dictionary<string, List<string>> _emotionToEmpathy;
        private readonly Random _rng;

        public SentimentDetector(Random rng = null)
        {
            _rng = rng ?? new Random();
            _emotionToEmpathy = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                ["worried"]    = new List<string> { "I understand it can feel overwhelming.", "It's natural to feel worried about this." },
                ["scared"]     = new List<string> { "I hear you — that fear is completely valid.", "Staying alert is a healthy reaction." },
                ["frustrated"] = new List<string> { "I get the frustration — security shouldn't be this hard.", "That sounds annoying — let's tackle it." },
                ["annoyed"]    = new List<string> { "Totally fair to be annoyed.", "I hear you — let's make this easier." },
                ["nervous"]    = new List<string> { "Being cautious is a strength here.", "It's okay to feel nervous about this." },
                ["anxious"]    = new List<string> { "Take a breath — we'll work through it.", "I understand the anxiety." },
                ["confused"]   = new List<string> { "Happy to clear that up.", "It's a confusing topic — let me help." },
                ["curious"]    = new List<string> { "Great curiosity — let's dive in.", "Love that you're asking." },
                ["happy"]      = new List<string> { "Glad you're feeling positive!", "Awesome — let's keep that momentum." }
            };
        }

        public string Detect(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
            string lower = input.ToLowerInvariant();
            foreach (var emotion in _emotionToEmpathy.Keys)
            {
                if (Regex.IsMatch(lower, $@"\b{Regex.Escape(emotion)}\b"))
                    return emotion;
            }
            return null;
        }

        public string GetEmpathyLine(string emotion)
        {
            if (emotion == null || !_emotionToEmpathy.ContainsKey(emotion)) return null;
            var lines = _emotionToEmpathy[emotion];
            return lines[_rng.Next(lines.Count)];
        }
    }
}
