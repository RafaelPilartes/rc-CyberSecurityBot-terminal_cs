using System;
using System.Globalization;
using System.Windows.Data;
using CyberSecurityBot.Core.Models;

namespace CyberSecurityBot.Wpf.Converters
{
    public class SenderToPrefixConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Sender s)
            {
                switch (s)
                {
                    case Sender.User: return "◀ ";
                    case Sender.System: return "! ";
                    default: return "▶ ";
                }
            }
            return "▶ ";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}
