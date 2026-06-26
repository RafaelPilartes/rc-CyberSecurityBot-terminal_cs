using System.Collections.Specialized;
using System.Windows.Controls;
using System.Windows.Input;
using CyberSecurityBot.Wpf.ViewModels;

namespace CyberSecurityBot.Wpf.Views
{
    public partial class ChatView : UserControl
    {
        public ChatView()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is ChatViewModel oldVm)
            {
                ((INotifyCollectionChanged)oldVm.Messages).CollectionChanged -= Messages_CollectionChanged;
            }
            if (e.NewValue is ChatViewModel newVm)
            {
                ((INotifyCollectionChanged)newVm.Messages).CollectionChanged += Messages_CollectionChanged;
            }
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
                if (DataContext is ChatViewModel vm)
                {
                    await vm.SendAsync();
                }
            }
        }
    }
}
