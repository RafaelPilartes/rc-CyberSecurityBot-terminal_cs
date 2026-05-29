using System.Collections.Specialized;
using System.IO;
using System.Windows;
using System.Windows.Input;
using CyberSecurityBot.Wpf.ViewModels;

namespace CyberSecurityBot.Wpf.Views
{
    public partial class MainWindow : Window
    {
        private readonly ChatViewModel _vm;

        public MainWindow(ChatViewModel vm, string asciiArtPath)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = vm;

            if (File.Exists(asciiArtPath))
            {
                AsciiArtBlock.Text = File.ReadAllText(asciiArtPath);
            }

            ((INotifyCollectionChanged)_vm.Messages).CollectionChanged += Messages_CollectionChanged;
            Loaded += (_, __) => { InputBox.Focus(); _vm.Initialise(); };
        }

        private void Messages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                ChatScroll.ScrollToEnd();
            }
        }

        private async void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && (Keyboard.Modifiers & ModifierKeys.Shift) == 0)
            {
                e.Handled = true;
                await _vm.SendAsync();
            }
        }
    }
}
