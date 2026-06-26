using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CyberSecurityBot.Core.Constants;
using CyberSecurityBot.Core.Models;
using CyberSecurityBot.Core.Services;
using CyberSecurityBot.Core.Services.Nlp;
using CyberSecurityBot.Core.Utilities;

namespace CyberSecurityBot.Wpf.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        private readonly ChatEngine _engine;
        private readonly IntentRecognizer _recognizer;
        private readonly TasksViewModel _tasks;
        private readonly QuizViewModel _quiz;
        private readonly ActivityLog _log;
        private string _inputText = string.Empty;
        private bool _isBusy;

        // Tab order in MainWindow: 0 Chat, 1 Tasks, 2 Quiz, 3 Activity Log.
        private const int TabTasks = 1;
        private const int TabQuiz = 2;
        private const int TabActivityLog = 3;

        public ObservableCollection<BotMessage> Messages { get; } = new ObservableCollection<BotMessage>();
        public ICommand SendCommand { get; }

        /// <summary>Set by the composition root to let the chat switch tabs.</summary>
        public Action<int> NavigateToTab { get; set; }

        public string InputText
        {
            get => _inputText;
            set { _inputText = value; OnPropertyChanged(); }
        }

        public bool IsBusy
        {
            get => _isBusy;
            private set { _isBusy = value; OnPropertyChanged(); }
        }

        public ChatViewModel(
            ChatEngine engine,
            IntentRecognizer recognizer,
            TasksViewModel tasks,
            QuizViewModel quiz,
            ActivityLog log)
        {
            _engine = engine ?? throw new ArgumentNullException(nameof(engine));
            _recognizer = recognizer ?? throw new ArgumentNullException(nameof(recognizer));
            _tasks = tasks ?? throw new ArgumentNullException(nameof(tasks));
            _quiz = quiz ?? throw new ArgumentNullException(nameof(quiz));
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _engine.OnSystemNotice = msg => Messages.Add(msg);
            SendCommand = new RelayCommand(async _ => await SendAsync(), _ => !IsBusy);
        }

        public void Initialise()
        {
            _engine.Initialise();
        }

        public async Task SendAsync()
        {
            if (IsBusy) return;
            string text = InputText ?? string.Empty;

            var validation = InputValidator.Validate(text);
            switch (validation)
            {
                case ValidationResult.Empty:
                    Messages.Add(new BotMessage { Sender = Sender.System, Text = BotResponses.EmptyInputNotice });
                    return;
                case ValidationResult.TooLong:
                    Messages.Add(new BotMessage { Sender = Sender.System, Text = BotResponses.TooLongInputNotice });
                    return;
            }

            Messages.Add(new BotMessage { Sender = Sender.User, Text = text });
            InputText = string.Empty;

            IsBusy = true;
            try
            {
                await Task.Delay(300);

                // Decision flow: try an NLP intent first; otherwise let the chat
                // engine handle sentiment / keywords / follow-ups / fallback.
                var intent = _recognizer.Recognize(text);
                BotMessage reply = intent.Intent != Intent.None
                    ? HandleIntent(intent)
                    : _engine.ProcessInput(text);

                Messages.Add(reply);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private BotMessage HandleIntent(IntentResult intent)
        {
            switch (intent.Intent)
            {
                case Intent.AddTask: return DoAddTask(intent);
                case Intent.ViewTasks: return DoViewTasks();
                case Intent.CompleteTask: return DoCompleteTask(intent);
                case Intent.StartQuiz: return DoStartQuiz();
                case Intent.ViewActivityLog: return DoViewActivityLog();
                default: return _engine.ProcessInput(intent.TaskTitle ?? string.Empty);
            }
        }

        private BotMessage DoAddTask(IntentResult intent)
        {
            var created = _tasks.AddTask(intent.TaskTitle);
            if (created == null)
            {
                return Bot("I couldn't save that task — is the database (MySQL) running?");
            }

            _log.Record($"NLP intent: added task \"{created.Title}\"");
            NavigateToTab?.Invoke(TabTasks);

            string message = $"Done — I've added the task \"{created.Title}\".";
            if (intent.AskForReminder)
            {
                message += " Would you like to set a reminder date? You can add one in the Tasks tab.";
            }
            return Bot(message);
        }

        private BotMessage DoViewTasks()
        {
            _tasks.Load();
            NavigateToTab?.Invoke(TabTasks);
            _log.Record("NLP intent: listed tasks");

            if (_tasks.Tasks.Count == 0)
            {
                return Bot("You have no tasks yet. Try: \"remind me to enable 2FA\".");
            }

            var sb = new StringBuilder("Here are your tasks:");
            foreach (var task in _tasks.Tasks)
            {
                string box = task.IsComplete ? "[x]" : "[ ]";
                sb.AppendLine();
                sb.Append($"{box} #{task.Id} — {task.Title}");
            }
            return Bot(sb.ToString());
        }

        private BotMessage DoCompleteTask(IntentResult intent)
        {
            if (intent.TaskId == null)
            {
                return Bot("Which task? Tell me its number, e.g. \"complete task 3\". You can see the numbers in the Tasks tab.");
            }

            bool ok = _tasks.CompleteById(intent.TaskId.Value);
            NavigateToTab?.Invoke(TabTasks);

            if (!ok)
            {
                return Bot($"I couldn't find a task with number {intent.TaskId}.");
            }

            _log.Record($"NLP intent: completed task #{intent.TaskId}");
            return Bot($"Nice — task #{intent.TaskId} is marked complete.");
        }

        private BotMessage DoStartQuiz()
        {
            _quiz.Start();
            NavigateToTab?.Invoke(TabQuiz);
            _log.Record("NLP intent: started quiz");
            return Bot("Starting the quiz — good luck! Head to the Quiz tab.");
        }

        private BotMessage DoViewActivityLog()
        {
            NavigateToTab?.Invoke(TabActivityLog);
            var recent = _log.GetRecent(10);
            if (recent.Count == 0)
            {
                return Bot("No activity recorded yet this session.");
            }

            var sb = new StringBuilder("Here's what I've done recently:");
            foreach (var entry in recent)
            {
                sb.AppendLine();
                sb.Append($"[{entry.Timestamp:HH:mm:ss}] {entry.Description}");
            }
            return Bot(sb.ToString());
        }

        private static BotMessage Bot(string text)
        {
            return new BotMessage { Sender = Sender.Bot, Text = text };
        }
    }
}
