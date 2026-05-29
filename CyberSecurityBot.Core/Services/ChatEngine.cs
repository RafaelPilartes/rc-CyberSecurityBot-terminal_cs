using System;
using System.Linq;
using System.Text;
using CyberSecurityBot.Core.Constants;
using CyberSecurityBot.Core.Models;

namespace CyberSecurityBot.Core.Services
{
    public class ChatEngine
    {
        private readonly ResponseRepository _repository;
        private readonly KeywordMatcher _matcher;
        private readonly SentimentDetector _sentiment;
        private readonly ConversationContext _context;
        private readonly MemoryStore _memory;
        private readonly Random _rng;

        public Action<BotMessage> OnSystemNotice { get; set; }

        public ChatEngine(
            ResponseRepository repository,
            KeywordMatcher matcher,
            SentimentDetector sentiment,
            ConversationContext context,
            MemoryStore memory,
            Random rng = null)
        {
            _repository = repository;
            _matcher = matcher;
            _sentiment = sentiment;
            _context = context;
            _memory = memory;
            _rng = rng ?? new Random();
        }

        public void Initialise()
        {
            var name = string.IsNullOrWhiteSpace(_memory.Memory.Name) ? "there" : _memory.Memory.Name;
            var topics = string.Join(", ", _repository.GetTopicKeys());
            string welcome = $"Welcome back, {name}! Ask me about: {topics}.";
            OnSystemNotice?.Invoke(new BotMessage { Sender = Sender.Bot, Text = welcome });
        }

        public BotMessage ProcessInput(string input)
        {
            try
            {
                string emotion = _sentiment.Detect(input);

                string prevName = _memory.Memory.Name;
                string prevFavTopic = _memory.Memory.FavouriteTopic;
                bool memoryChanged = _memory.CaptureFacts(input, _repository.GetTopicKeys());
                bool nameJustChanged = memoryChanged
                    && !string.Equals(prevName, _memory.Memory.Name, StringComparison.OrdinalIgnoreCase);
                bool favTopicJustChanged = memoryChanged
                    && !string.Equals(prevFavTopic, _memory.Memory.FavouriteTopic, StringComparison.OrdinalIgnoreCase);

                Response topic;
                if (_context.IsFollowUp(input) && _context.LastResponse != null)
                {
                    topic = _context.LastResponse;
                }
                else
                {
                    topic = _matcher.FindBestMatch(input, _repository.All);
                }

                var sb = new StringBuilder();

                if (emotion != null)
                {
                    var line = _sentiment.GetEmpathyLine(emotion);
                    if (!string.IsNullOrEmpty(line)) sb.AppendLine(line);
                }

                if (topic == null)
                {
                    if (_context.IsFollowUp(input))
                    {
                        sb.AppendLine(BotResponses.FollowupWithoutContext);
                    }
                    else if (nameJustChanged || favTopicJustChanged)
                    {
                        if (nameJustChanged)
                            sb.AppendLine($"Nice to meet you, {_memory.Memory.Name}! I'll remember that.");
                        if (favTopicJustChanged)
                            sb.AppendLine($"Got it — I'll keep your interest in {_memory.Memory.FavouriteTopic} in mind.");
                        sb.Append("Ask me about: ");
                        sb.Append(string.Join(", ", _repository.GetTopicKeys()));
                        sb.Append(".");
                    }
                    else
                    {
                        sb.Append(BotResponses.FallbackPrefix);
                        sb.Append(string.Join(", ", _repository.GetTopicKeys()));
                        sb.Append(".");
                    }
                }
                else
                {
                    if (nameJustChanged)
                    {
                        sb.AppendLine($"Nice to meet you, {_memory.Memory.Name}!");
                    }


                    if (!string.IsNullOrEmpty(_memory.Memory.FavouriteTopic)
                        && !string.Equals(_memory.Memory.FavouriteTopic, topic.topicKey, StringComparison.OrdinalIgnoreCase)
                        && _rng.NextDouble() < 0.30)
                    {
                        sb.AppendLine($"As someone interested in {_memory.Memory.FavouriteTopic}, you might also want to know:");
                    }

                    string reply = _context.PickUnusedReply(topic, _rng);
                    if (!string.IsNullOrEmpty(reply))
                    {
                        sb.AppendLine(reply);
                        _context.MarkReplyUsed(reply);
                    }

                    if (topic.followups != null && topic.followups.Length > 0)
                    {
                        sb.AppendLine();
                        sb.Append("You might also ask: ");
                        sb.Append(topic.followups[_rng.Next(topic.followups.Length)]);
                    }

                    _context.SetTopic(topic);
                    _memory.RecordTopic(topic.topicKey);
                }

                if (memoryChanged || topic != null) _memory.Save();

                return new BotMessage { Sender = Sender.Bot, Text = sb.ToString().TrimEnd() };
            }
            catch (Exception)
            {
                return new BotMessage { Sender = Sender.System, Text = BotResponses.EngineError };
            }
        }
    }
}
