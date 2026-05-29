using System;
using System.Collections.Generic;
using CyberSecurityBot.Core.Models;

namespace CyberSecurityBot.Core.Services
{
    public class ConversationContext
    {
        private static readonly List<string> FollowUpTriggers = new List<string>
        {
            "tell me more", "another tip", "explain more", "more info", "more details",
            "go on", "continue", "more please"
        };

        public string LastTopicKey { get; private set; }
        public Response LastResponse { get; private set; }
        public List<string> UsedRepliesForTopic { get; } = new List<string>();

        public bool IsFollowUp(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            string lower = input.ToLowerInvariant().Trim();
            if (lower == "more") return true;
            foreach (var trigger in FollowUpTriggers)
            {
                if (lower.Contains(trigger)) return true;
            }
            return false;
        }

        public void SetTopic(Response response)
        {
            if (response == null) return;
            if (response.topicKey != LastTopicKey)
            {
                UsedRepliesForTopic.Clear();
            }
            LastTopicKey = response.topicKey;
            LastResponse = response;
        }

        public void MarkReplyUsed(string reply)
        {
            if (!string.IsNullOrEmpty(reply) && !UsedRepliesForTopic.Contains(reply))
                UsedRepliesForTopic.Add(reply);
        }

        public string PickUnusedReply(Response response, Random rng)
        {
            if (response?.replies == null || response.replies.Length == 0) return null;
            var unused = new List<string>();
            foreach (var r in response.replies)
                if (!UsedRepliesForTopic.Contains(r)) unused.Add(r);

            if (unused.Count == 0)
            {
                UsedRepliesForTopic.Clear();
                unused.AddRange(response.replies);
            }
            return unused[rng.Next(unused.Count)];
        }
    }
}
