namespace CyberSecurityBot.Wpf.ViewModels
{
    /// <summary>
    /// Root ViewModel that holds one child ViewModel per tab. Bound to the
    /// MainWindow's TabControl.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private int _selectedTabIndex;

        public ChatViewModel Chat { get; }
        public TasksViewModel Tasks { get; }
        public QuizViewModel Quiz { get; }
        public ActivityLogViewModel ActivityLog { get; }

        /// <summary>Bound to the TabControl so the chat can switch tabs (NLP navigation).</summary>
        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set { _selectedTabIndex = value; OnPropertyChanged(); }
        }

        public MainViewModel(
            ChatViewModel chat,
            TasksViewModel tasks,
            QuizViewModel quiz,
            ActivityLogViewModel activityLog)
        {
            Chat = chat;
            Tasks = tasks;
            Quiz = quiz;
            ActivityLog = activityLog;
        }
    }
}
