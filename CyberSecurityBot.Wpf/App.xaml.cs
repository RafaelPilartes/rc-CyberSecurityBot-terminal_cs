using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using CyberSecurityBot.Core.Services;
using CyberSecurityBot.Core.Services.Data;
using CyberSecurityBot.Wpf.ViewModels;
using CyberSecurityBot.Wpf.Views;

namespace CyberSecurityBot.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            DispatcherUnhandledException += OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += OnDomainUnhandledException;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string responsesPath = Path.Combine(baseDir, "Assets", "responses.json");
            string wavPath = Path.Combine(baseDir, "Assets", "greeting.wav");
            string asciiPath = Path.Combine(baseDir, "Assets", "AsciiArt.txt");
            string dbConfigPath = Path.Combine(baseDir, "Assets", "dbconfig.json");

            ResponseRepository repository;
            try
            {
                repository = new ResponseRepository(responsesPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load responses: {ex.Message}\n\nThe app will exit.",
                    "Cyber Bot", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown(1);
                return;
            }

            var rng = new Random();
            var matcher = new KeywordMatcher(rng);
            var sentiment = new SentimentDetector(rng);
            var context = new ConversationContext();
            var memory = new MemoryStore(MemoryStore.DefaultPath());
            memory.Load();

            var player = new AudioPlayer();
            Task.Run(() => player.PlayWav(wavPath));

            if (string.IsNullOrWhiteSpace(memory.Memory.Name))
            {
                var dialog = new NameDialog();
                dialog.ShowDialog();
                memory.Memory.Name = dialog.EnteredName;
                memory.Save();
            }

            // Shared session activity log (Part 3 Task 4), used by the engine,
            // the tasks tab and the activity-log tab.
            var activityLog = new ActivityLog();

            var engine = new ChatEngine(repository, matcher, sentiment, context, memory, activityLog, rng);

            // Database layer (Part 3). Connection string is loaded from a config
            // file so it is never hardcoded in the middle of the code.
            var dbConfig = DatabaseConfig.Load(dbConfigPath);
            var taskRepository = new TaskRepository(dbConfig);

            // Create the database/table if needed. Wrapped so the app still
            // launches (chat/quiz/log keep working) when MySQL is not running.
            try
            {
                new DatabaseInitializer(dbConfig).EnsureCreated();
            }
            catch (Exception dbEx)
            {
                MessageBox.Show(
                    "Could not reach the database (is MySQL running in XAMPP?).\n\n" +
                    "The Tasks tab will be unavailable, but the rest of the app works.\n\n" +
                    dbEx.Message,
                    "Cyber Bot", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            var chatVm = new ChatViewModel(engine);
            var tasksVm = new TasksViewModel(taskRepository, activityLog);
            tasksVm.Load();
            var quizVm = new QuizViewModel();
            var logVm = new ActivityLogViewModel(activityLog);
            var vm = new MainViewModel(chatVm, tasksVm, quizVm, logVm);

            var window = new MainWindow(vm, asciiPath);
            MainWindow = window;
            window.Show();

            ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"Something went wrong: {e.Exception.Message}", "Cyber Bot",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            e.Handled = true;
        }

        private void OnDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                MessageBox.Show($"Fatal error: {ex.Message}", "Cyber Bot",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
