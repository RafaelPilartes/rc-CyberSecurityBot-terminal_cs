using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CyberSecurityBot.Wpf.ViewModels
{
    /// <summary>
    /// Quiz tab. STRUCTURE ONLY — commands are no-ops until Part 3 Task 2 logic
    /// is implemented.
    /// </summary>
    public class QuizViewModel : ViewModelBase
    {
        private string _scoreText = "Score: 0 / 0";
        private string _questionText = "Press Restart to start the quiz.";
        private string _feedbackText = string.Empty;

        public ObservableCollection<string> Options { get; } = new ObservableCollection<string>();

        public string ScoreText
        {
            get => _scoreText;
            set { _scoreText = value; OnPropertyChanged(); }
        }

        public string QuestionText
        {
            get => _questionText;
            set { _questionText = value; OnPropertyChanged(); }
        }

        public string FeedbackText
        {
            get => _feedbackText;
            set { _feedbackText = value; OnPropertyChanged(); }
        }

        public ICommand NextCommand { get; }
        public ICommand RestartCommand { get; }

        public QuizViewModel()
        {
            NextCommand = new RelayCommand(_ => { /* Part 3 Task 2 — logic added later */ });
            RestartCommand = new RelayCommand(_ => { /* Part 3 Task 2 — logic added later */ });
        }
    }
}
