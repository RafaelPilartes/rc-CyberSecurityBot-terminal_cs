using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CyberSecurityBot.Core.Models;
using CyberSecurityBot.Core.Services;
using CyberSecurityBot.Core.Services.Data;

namespace CyberSecurityBot.Wpf.ViewModels
{
    /// <summary>
    /// Tasks tab — add, list, complete and delete cybersecurity tasks persisted
    /// in MySQL (Part 3 Task 1).
    /// </summary>
    public class TasksViewModel : ViewModelBase
    {
        private readonly TaskRepository _repository;
        private readonly ActivityLog _log;

        private string _newTitle = string.Empty;
        private string _newDescription = string.Empty;
        private string _newReminder = string.Empty;
        private DateTime? _newReminderDate;
        private string _statusMessage = string.Empty;

        public ObservableCollection<CyberTask> Tasks { get; } = new ObservableCollection<CyberTask>();

        public string NewTitle
        {
            get => _newTitle;
            set { _newTitle = value; OnPropertyChanged(); }
        }

        public string NewDescription
        {
            get => _newDescription;
            set { _newDescription = value; OnPropertyChanged(); }
        }

        public string NewReminder
        {
            get => _newReminder;
            set { _newReminder = value; OnPropertyChanged(); }
        }

        public DateTime? NewReminderDate
        {
            get => _newReminderDate;
            set { _newReminderDate = value; OnPropertyChanged(); }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; }
        public ICommand CompleteCommand { get; }
        public ICommand DeleteCommand { get; }

        public TasksViewModel(TaskRepository repository, ActivityLog log)
        {
            _repository = repository;
            _log = log;
            AddCommand = new RelayCommand(_ => Add(), _ => !string.IsNullOrWhiteSpace(NewTitle));
            CompleteCommand = new RelayCommand(p => Complete(p as CyberTask));
            DeleteCommand = new RelayCommand(p => Delete(p as CyberTask));
        }

        /// <summary>Loads all tasks from the database into the list.</summary>
        public void Load()
        {
            try
            {
                Tasks.Clear();
                foreach (var task in _repository.GetAll())
                {
                    Tasks.Add(task);
                }
                StatusMessage = Tasks.Count == 0 ? "No tasks yet. Add one above." : string.Empty;
            }
            catch (Exception ex)
            {
                StatusMessage = "Database unavailable — is MySQL running in XAMPP? " + ex.Message;
            }
        }

        private void Add()
        {
            var created = AddTask(NewTitle, NewDescription, NewReminder, NewReminderDate);
            if (created != null)
            {
                NewTitle = string.Empty;
                NewDescription = string.Empty;
                NewReminder = string.Empty;
                NewReminderDate = null;
            }
        }

        /// <summary>
        /// Adds a task (used both by the form and by the NLP chat commands).
        /// Returns the created task, or null if it could not be saved.
        /// </summary>
        public CyberTask AddTask(string title, string description = null,
                                 string reminder = null, DateTime? reminderDate = null)
        {
            if (string.IsNullOrWhiteSpace(title)) return null;
            try
            {
                var task = new CyberTask
                {
                    Title = title.Trim(),
                    Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim(),
                    Reminder = string.IsNullOrWhiteSpace(reminder) ? null : reminder.Trim(),
                    ReminderDate = reminderDate,
                    IsComplete = false
                };
                task.Id = _repository.Add(task);

                _log?.Record($"Task added: \"{task.Title}\"");
                if (task.ReminderDate.HasValue || !string.IsNullOrWhiteSpace(task.Reminder))
                {
                    _log?.Record($"Reminder set for task: \"{task.Title}\"");
                }

                Load();
                return task;
            }
            catch (Exception ex)
            {
                StatusMessage = "Could not add the task: " + ex.Message;
                return null;
            }
        }

        /// <summary>Marks a task complete by id. Returns false if not found / failed.</summary>
        public bool CompleteById(int id)
        {
            try
            {
                Load();
                if (Tasks.All(t => t.Id != id)) return false;

                _repository.MarkComplete(id);
                _log?.Record($"Task marked complete (id {id})");
                Load();
                return true;
            }
            catch (Exception ex)
            {
                StatusMessage = "Could not update the task: " + ex.Message;
                return false;
            }
        }

        private void Complete(CyberTask task)
        {
            if (task == null) return;
            CompleteById(task.Id);
        }

        private void Delete(CyberTask task)
        {
            if (task == null) return;
            try
            {
                _repository.Delete(task.Id);
                _log?.Record($"Task deleted (id {task.Id})");
                Load();
            }
            catch (Exception ex)
            {
                StatusMessage = "Could not delete the task: " + ex.Message;
            }
        }
    }
}
