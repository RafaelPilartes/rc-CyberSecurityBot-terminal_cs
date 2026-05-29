using System.Windows;
using System.Windows.Input;

namespace CyberSecurityBot.Wpf.Views
{
    public partial class NameDialog : Window
    {
        public string EnteredName { get; private set; } = "Guest";

        public NameDialog(string suggestedName = null)
        {
            InitializeComponent();
            if (!string.IsNullOrWhiteSpace(suggestedName)) NameInput.Text = suggestedName;
            Loaded += (_, __) => NameInput.Focus();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            var trimmed = NameInput.Text?.Trim();
            EnteredName = string.IsNullOrWhiteSpace(trimmed) ? "Guest" : trimmed;
            DialogResult = true;
            Close();
        }

        private void Skip_Click(object sender, RoutedEventArgs e)
        {
            EnteredName = "Guest";
            DialogResult = true;
            Close();
        }

        private void NameInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Ok_Click(sender, e);
        }
    }
}
