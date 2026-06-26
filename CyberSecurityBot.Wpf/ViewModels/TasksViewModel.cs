using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CyberSecurityBot.Core.Models;
using CyberSecurityBot.Core.Services.Data;

namespace CyberSecurityBot.Wpf.ViewModels
{
    /// <summary>
    /// Tasks tab. STRUCTURE ONLY — commands are no-ops until Part 3 Task 1 logic
    /// is implemented.
    /// </summary>
    public class TasksViewModel : ViewModelBase
    {
        private readonly TaskRepository _repository;

        private string _newTitle = string.Empty;
        private string _newDescription = string.Empty;
        private string _newReminder = string.Empty;
        private DateTime? _newReminderDate;

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

        public ICommand AddCommand { get; }

        public TasksViewModel(TaskRepository repository)
        {
            _repository = repository;
            AddCommand = new RelayCommand(_ => { /* Part 3 Task 1 — logic added later */ });
        }
    }
}
