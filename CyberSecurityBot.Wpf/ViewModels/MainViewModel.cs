namespace CyberSecurityBot.Wpf.ViewModels
{
    /// <summary>
    /// Root ViewModel that holds one child ViewModel per tab. Bound to the
    /// MainWindow's TabControl.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public ChatViewModel Chat { get; }
        public TasksViewModel Tasks { get; }
        public QuizViewModel Quiz { get; }
        public ActivityLogViewModel ActivityLog { get; }

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
