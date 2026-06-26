using System;
using System.Text.RegularExpressions;

namespace CyberSecurityBot.Core.Services.Nlp
{
    public enum Intent
    {
        None,
        AddTask,
        ViewTasks,
        CompleteTask,
        StartQuiz,
        ViewActivityLog
    }

    /// <summary>The outcome of recognising a user message.</summary>
    public class IntentResult
    {
        public Intent Intent { get; set; }

        /// <summary>Extracted task title (AddTask only).</summary>
        public string TaskTitle { get; set; }

        /// <summary>Extracted task id (CompleteTask only), if a number was given.</summary>
        public int? TaskId { get; set; }

        /// <summary>
        /// True when the phrasing was a plain "add a task" (so the bot should ask
        /// whether to set a reminder); false when a reminder was already implied
        /// (e.g. "remind me to ...").
        /// </summary>
        public bool AskForReminder { get; set; }

        public static IntentResult None_ => new IntentResult { Intent = Intent.None };
    }

    /// <summary>
    /// A lightweight NLP simulation (Part 3 Task 3). It detects the user's intent
    /// from differently phrased requests using <see cref="string.Contains(string)"/>
    /// and keyword lists per intent, and extracts the task title / id where relevant.
    /// </summary>
    public class IntentRecognizer
    {
        private static readonly string[] ViewLogPhrases =
        {
            "activity log", "show log", "what have you done", "recent actions", "history"
        };

        private static readonly string[] CompleteTaskPhrases =
        {
            "complete task", "mark task done", "mark task as done", "mark task complete",
            "i finished task", "finished task", "mark task"
        };

        private static readonly string[] ViewTaskPhrases =
        {
            "show my tasks", "what are my tasks", "list tasks", "pending tasks",
            "show tasks", "my tasks"
        };

        private static readonly string[] StartQuizPhrases =
        {
            "quiz", "test me", "test my knowledge", "trivia"
        };

        // Add-task triggers, longest/most specific first so the title is extracted
        // cleanly. AskReminder is false when the phrasing already implies a reminder.
        private static readonly AddTrigger[] AddTriggers =
        {
            new AddTrigger("add a task to", true),
            new AddTrigger("add a task", true),
            new AddTrigger("create a reminder to", false),
            new AddTrigger("create a reminder", false),
            new AddTrigger("set a reminder to", false),
            new AddTrigger("remind me to", false),
            new AddTrigger("don't forget to", false),
            new AddTrigger("dont forget to", false),
            new AddTrigger("don't forget", false),
            new AddTrigger("dont forget", false),
            new AddTrigger("i need to", true)
        };

        public IntentResult Recognize(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return IntentResult.None_;

            string text = input.ToLowerInvariant();

            if (ContainsAny(text, ViewLogPhrases))
                return new IntentResult { Intent = Intent.ViewActivityLog };

            if (ContainsAny(text, CompleteTaskPhrases))
                return new IntentResult { Intent = Intent.CompleteTask, TaskId = ExtractNumber(text) };

            if (ContainsAny(text, ViewTaskPhrases))
                return new IntentResult { Intent = Intent.ViewTasks };

            foreach (var trigger in AddTriggers)
            {
                int idx = text.IndexOf(trigger.Phrase, StringComparison.Ordinal);
                if (idx < 0) continue;

                string title = CleanTitle(input.Substring(idx + trigger.Phrase.Length));
                if (!string.IsNullOrWhiteSpace(title))
                {
                    return new IntentResult
                    {
                        Intent = Intent.AddTask,
                        TaskTitle = title,
                        AskForReminder = trigger.AskReminder
                    };
                }
            }

            if (ContainsAny(text, StartQuizPhrases))
                return new IntentResult { Intent = Intent.StartQuiz };

            return IntentResult.None_;
        }

        private static bool ContainsAny(string text, string[] phrases)
        {
            foreach (var phrase in phrases)
            {
                if (text.Contains(phrase)) return true;
            }
            return false;
        }

        private static int? ExtractNumber(string text)
        {
            var match = Regex.Match(text, @"\d+");
            if (match.Success && int.TryParse(match.Value, out int n)) return n;
            return null;
        }

        private static string CleanTitle(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return null;
            string t = raw.Trim().TrimEnd('.', '!', '?', ',', ';');
            if (t.Length == 0) return null;
            return char.ToUpper(t[0]) + t.Substring(1);
        }

        private struct AddTrigger
        {
            public readonly string Phrase;
            public readonly bool AskReminder;

            public AddTrigger(string phrase, bool askReminder)
            {
                Phrase = phrase;
                AskReminder = askReminder;
            }
        }
    }
}
