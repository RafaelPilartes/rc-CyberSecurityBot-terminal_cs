using System.Collections.ObjectModel;
using CyberSecurityBot.Core.Models;

namespace CyberSecurityBot.Wpf.ViewModels
{
    /// <summary>
    /// Activity Log tab. STRUCTURE ONLY — populated by Part 3 Task 4 logic later.
    /// </summary>
    public class ActivityLogViewModel : ViewModelBase
    {
        public ObservableCollection<ActivityLogEntry> Entries { get; } =
            new ObservableCollection<ActivityLogEntry>();
    }
}
