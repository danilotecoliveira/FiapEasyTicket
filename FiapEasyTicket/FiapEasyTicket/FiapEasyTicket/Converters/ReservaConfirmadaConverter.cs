using System;
using Xamarin.Forms;
using System.Globalization;

namespace FiapEasyTicket.Converters
{
    class ReservaConfirmadaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var confirmado = (bool)value;
            return (confirmado) ? Color.Black : Color.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
