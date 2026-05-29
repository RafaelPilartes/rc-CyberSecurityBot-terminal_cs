using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using CyberSecurityBot.Core.Models;

namespace CyberSecurityBot.Wpf.Converters
{
    public class SenderToForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string key = "PrimaryBrush";
            if (value is Sender s)
            {
                if (s == Sender.User) key = "UserForegroundBrush";
                else if (s == Sender.System) key = "SystemForegroundBrush";
            }
            return Application.Current.Resources[key] as Brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}
