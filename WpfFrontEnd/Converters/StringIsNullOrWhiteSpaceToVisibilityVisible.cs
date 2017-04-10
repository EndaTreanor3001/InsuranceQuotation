using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfFrontEnd.Converters
{
    public class StringIsNullOrWhiteSpaceToVisibilityVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueAsString = value as string;
            if(!string.IsNullOrWhiteSpace(valueAsString)) return Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
