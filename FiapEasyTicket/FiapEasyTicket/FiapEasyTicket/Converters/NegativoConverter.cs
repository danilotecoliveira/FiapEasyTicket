using System;
using Xamarin.Forms;
using System.Globalization;

namespace FiapEasyTicket.Converters
{
    public class NegativoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}
