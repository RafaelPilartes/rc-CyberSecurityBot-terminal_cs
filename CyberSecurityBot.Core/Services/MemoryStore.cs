using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CyberSecurityBot.Core.Models;
using Newtonsoft.Json;

namespace CyberSecurityBot.Core.Services
{
    public class MemoryStore
    {
        private readonly string _filePath;
        public UserMemory Memory { get; private set; } = new UserMemory();

        public MemoryStore(string filePath)
        {
            _filePath = filePath;
        }

        public static string DefaultPath()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, "CyberSecurityBot", "user_memory.json");
        }

        public void Load()
        {
            try
            {
                if (!File.Exists(_filePath)) return;
                string json = File.ReadAllText(_filePath);
                var loaded = JsonConvert.DeserializeObject<UserMemory>(json);
                if (loaded != null) Memory = loaded;
                if (Memory.DiscussedTopics == null) Memory.DiscussedTopics = new List<string>();
            }
            catch
            {
                Memory = new UserMemory();
            }
        }

        public void Save()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_filePath));
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(Memory, Formatting.Indented));
            }
            catch
            {
                // silent — memory failures shouldn't crash the bot
            }
        }

        public void RecordTopic(string topicKey)
        {
            if (string.IsNullOrWhiteSpace(topicKey)) return;
            if (!Memory.DiscussedTopics.Contains(topicKey))
                Memory.DiscussedTopics.Add(topicKey);
        }

        public bool CaptureFacts(string input, IReadOnlyList<string> knownTopicKeys)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            bool changed = false;

            var nameMatch = Regex.Match(input,
                @"\b(?:my name is|i am|i'm|call me)\s+([A-Za-z][A-Za-z'\-]{1,30})\b",
                RegexOptions.IgnoreCase);
            if (nameMatch.Success)
            {
                string name = nameMatch.Groups[1].Value;
                if (!string.Equals(name, Memory.Name, StringComparison.OrdinalIgnoreCase))
                {
                    Memory.Name = name;
                    changed = true;
                }
            }

            var topicMatch = Regex.Match(input,
                @"\b(?:interested in|care about|love|focus on)\s+([a-zA-Z\s]{2,30})\b",
                RegexOptions.IgnoreCase);
            if (topicMatch.Success)
            {
                string candidate = topicMatch.Groups[1].Value.Trim().ToLowerInvariant();
                var match = knownTopicKeys?.FirstOrDefault(k =>
                    candidate.Contains(k.ToLowerInvariant()));
                if (match != null && !string.Equals(match, Memory.FavouriteTopic, StringComparison.OrdinalIgnoreCase))
                {
                    Memory.FavouriteTopic = match;
                    changed = true;
                }
            }

            return changed;
        }
    }
}
