using System;
using System.Collections.Generic;
using System.Linq;
using CyberSecurityBot.Core.Models;

namespace CyberSecurityBot.Core.Services
{
    /// <summary>
    /// In-memory log of the significant actions the bot takes during a session
    /// (Part 3 Task 4). Each entry is timestamped. Raises <see cref="Changed"/>
    /// whenever a new action is recorded so the UI can refresh live.
    /// </summary>
    public class ActivityLog
    {
        private readonly List<ActivityLogEntry> _entries = new List<ActivityLogEntry>();

        /// <summary>Fired after a new entry is recorded.</summary>
        public event Action Changed;

        public void Record(string description)
        {
            if (string.IsNullOrWhiteSpace(description)) return;
            _entries.Add(new ActivityLogEntry
            {
                Timestamp = DateTime.Now,
                Description = description
            });
            Changed?.Invoke();
        }

        /// <summary>Returns the most recent actions, newest first (default 10).</summary>
        public IReadOnlyList<ActivityLogEntry> GetRecent(int count = 10)
        {
            return _entries
                .AsEnumerable()
                .Reverse()
                .Take(count)
                .ToList();
        }
    }
}
