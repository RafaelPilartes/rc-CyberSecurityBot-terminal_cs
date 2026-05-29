using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using CyberSecurityBot.Core.Models;

namespace CyberSecurityBot.Wpf.Converters
{
    public class SenderToAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Sender s)
            {
                switch (s)
                {
                    case Sender.User: return HorizontalAlignment.Right;
                    case Sender.System: return HorizontalAlignment.Center;
                    default: return HorizontalAlignment.Left;
                }
            }
            return HorizontalAlignment.Left;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}
