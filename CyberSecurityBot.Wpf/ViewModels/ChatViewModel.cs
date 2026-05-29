using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CyberSecurityBot.Core.Constants;
using CyberSecurityBot.Core.Models;
using CyberSecurityBot.Core.Services;
using CyberSecurityBot.Core.Utilities;

namespace CyberSecurityBot.Wpf.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        private readonly ChatEngine _engine;
        private string _inputText = string.Empty;
        private bool _isBusy;

        public ObservableCollection<BotMessage> Messages { get; } = new ObservableCollection<BotMessage>();
        public ICommand SendCommand { get; }

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

        public ChatViewModel(ChatEngine engine)
        {
            _engine = engine ?? throw new ArgumentNullException(nameof(engine));
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
                var reply = _engine.ProcessInput(text);
                Messages.Add(reply);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
