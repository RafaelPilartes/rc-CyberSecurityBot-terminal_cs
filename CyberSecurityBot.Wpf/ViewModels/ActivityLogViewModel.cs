using System.Collections.ObjectModel;
using CyberSecurityBot.Core.Models;
using CyberSecurityBot.Core.Services;

namespace CyberSecurityBot.Wpf.ViewModels
{
    /// <summary>
    /// Activity Log tab — shows the most recent actions the bot has taken
    /// (Part 3 Task 4). Refreshes live as new actions are recorded.
    /// </summary>
    public class ActivityLogViewModel : ViewModelBase
    {
        private readonly ActivityLog _log;

        public ObservableCollection<ActivityLogEntry> Entries { get; } =
            new ObservableCollection<ActivityLogEntry>();

        public ActivityLogViewModel(ActivityLog log)
        {
            _log = log;
            _log.Changed += Refresh;
            Refresh();
        }

        private void Refresh()
        {
            Entries.Clear();
            foreach (var entry in _log.GetRecent(10))
            {
                Entries.Add(entry);
            }
        }
    }
}
