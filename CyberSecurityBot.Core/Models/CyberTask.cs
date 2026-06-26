using System;

namespace CyberSecurityBot.Core.Models
{
    /// <summary>
    /// A cybersecurity task created by the user (e.g. "Enable two-factor authentication").
    /// Maps to a row in the MySQL `tasks` table.
    /// </summary>
    public class CyberTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        /// <summary>Free-text reminder note (optional).</summary>
        public string Reminder { get; set; }

        /// <summary>Optional date/time the reminder is due.</summary>
        public DateTime? ReminderDate { get; set; }

        public bool IsComplete { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
