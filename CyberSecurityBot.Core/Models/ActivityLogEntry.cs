using System;

namespace CyberSecurityBot.Core.Models
{
    /// <summary>
    /// A single significant action the bot performed during the session,
    /// shown in the Activity Log tab.
    /// </summary>
    public class ActivityLogEntry
    {
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
    }
}
