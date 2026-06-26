using System.IO;
using System.Windows;
using CyberSecurityBot.Wpf.ViewModels;

namespace CyberSecurityBot.Wpf.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _vm;

        public MainWindow(MainViewModel vm, string asciiArtPath)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = vm;

            if (File.Exists(asciiArtPath))
            {
                AsciiArtBlock.Text = File.ReadAllText(asciiArtPath);
            }

            Loaded += (_, __) => _vm.Chat.Initialise();
        }
    }
}
