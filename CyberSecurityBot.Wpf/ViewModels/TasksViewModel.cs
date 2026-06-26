using System;
using System.Collections.ObjectModel;
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
            try
            {
                var task = new CyberTask
                {
                    Title = NewTitle.Trim(),
                    Description = string.IsNullOrWhiteSpace(NewDescription) ? null : NewDescription.Trim(),
                    Reminder = string.IsNullOrWhiteSpace(NewReminder) ? null : NewReminder.Trim(),
                    ReminderDate = NewReminderDate,
                    IsComplete = false
                };
                _repository.Add(task);

                _log?.Record($"Task added: \"{task.Title}\"");
                if (task.ReminderDate.HasValue || !string.IsNullOrWhiteSpace(task.Reminder))
                {
                    _log?.Record($"Reminder set for task: \"{task.Title}\"");
                }

                NewTitle = string.Empty;
                NewDescription = string.Empty;
                NewReminder = string.Empty;
                NewReminderDate = null;

                Load();
            }
            catch (Exception ex)
            {
                StatusMessage = "Could not add the task: " + ex.Message;
            }
        }

        private void Complete(CyberTask task)
        {
            if (task == null) return;
            try
            {
                _repository.MarkComplete(task.Id);
                _log?.Record($"Task marked complete (id {task.Id})");
                Load();
            }
            catch (Exception ex)
            {
                StatusMessage = "Could not update the task: " + ex.Message;
            }
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
