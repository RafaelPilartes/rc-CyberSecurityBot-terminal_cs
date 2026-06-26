using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CyberSecurityBot.Core.Models;
using CyberSecurityBot.Core.Services;

namespace CyberSecurityBot.Wpf.ViewModels
{
    /// <summary>
    /// Quiz tab (Part 3 Task 2). Shows one question at a time in a random order,
    /// gives immediate feedback with an explanation, tracks the score and shows a
    /// final result. Logs quiz start/finish to the activity log.
    /// </summary>
    public class QuizViewModel : ViewModelBase
    {
        private readonly QuizBank _bank;
        private readonly ActivityLog _log;
        private readonly Random _rng;

        private List<QuizQuestion> _questions = new List<QuizQuestion>();
        private int _index;
        private int _score;
        private bool _started;
        private bool _answered;
        private bool _finished;

        private string _scoreText = string.Empty;
        private string _questionText = "Press Start to begin the quiz.";
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

        public ICommand AnswerCommand { get; }
        public ICommand NextCommand { get; }
        public ICommand RestartCommand { get; }

        public QuizViewModel(QuizBank bank, ActivityLog log, Random rng = null)
        {
            _bank = bank;
            _log = log;
            _rng = rng ?? new Random();

            AnswerCommand = new RelayCommand(p => Answer(p as string), _ => _started && !_answered && !_finished);
            NextCommand = new RelayCommand(_ => Next(), _ => _started && _answered && !_finished);
            RestartCommand = new RelayCommand(_ => Start());
        }

        /// <summary>Starts (or restarts) the quiz. Public so the NLP chat command can trigger it.</summary>
        public void Start()
        {
            _questions = _bank.GetQuestions().OrderBy(_ => _rng.Next()).ToList();
            _index = 0;
            _score = 0;
            _started = true;
            _finished = false;
            _log?.Record("Quiz started");
            LoadCurrent();
        }

        private void LoadCurrent()
        {
            _answered = false;
            FeedbackText = string.Empty;

            var q = _questions[_index];
            QuestionText = q.Text;
            Options.Clear();
            foreach (var option in q.Options) Options.Add(option);

            ScoreText = $"Question {_index + 1} / {_questions.Count}  —  Score: {_score}";
            Refresh();
        }

        private void Answer(string selected)
        {
            if (!_started || _answered || _finished || selected == null) return;

            var q = _questions[_index];
            string correctOption = q.Options[q.CorrectIndex];
            bool correct = string.Equals(selected, correctOption, StringComparison.Ordinal);

            if (correct) _score++;
            _answered = true;

            FeedbackText = (correct
                ? "Correct! "
                : $"Not quite. The answer is \"{correctOption}\". ") + q.Explanation;
            ScoreText = $"Question {_index + 1} / {_questions.Count}  —  Score: {_score}";
            Refresh();
        }

        private void Next()
        {
            if (!_answered) return;
            _index++;
            if (_index >= _questions.Count)
            {
                Finish();
                return;
            }
            LoadCurrent();
        }

        private void Finish()
        {
            _finished = true;
            _started = false;
            Options.Clear();

            int total = _questions.Count;
            QuestionText = $"Quiz complete! You scored {_score} / {total}.";
            FeedbackText = BuildFeedback(_score, total);
            ScoreText = $"Final score: {_score} / {total}";
            _log?.Record($"Quiz finished (score {_score}/{total})");
            Refresh();
        }

        private static string BuildFeedback(int score, int total)
        {
            double pct = total == 0 ? 0 : (double)score / total;
            if (pct >= 0.9) return "Outstanding! You're a cybersecurity pro!";
            if (pct >= 0.7) return "Great job! You've got solid security habits.";
            if (pct >= 0.5) return "Not bad — but there's room to sharpen your defences.";
            return "Keep learning! Review the tips and try the quiz again.";
        }

        private static void Refresh()
        {
            // Let WPF re-evaluate the commands so buttons enable/disable immediately.
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
