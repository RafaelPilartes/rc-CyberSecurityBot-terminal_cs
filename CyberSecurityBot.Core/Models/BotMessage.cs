using System;

namespace CyberSecurityBot.Core.Models
{
    public class BotMessage
    {
        public Sender Sender { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
