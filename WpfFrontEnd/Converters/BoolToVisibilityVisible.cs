using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfFrontEnd.Converters
{
    public class BoolToVisibilityVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var objectAsBool = value as bool?;
            if(objectAsBool.HasValue && objectAsBool.Value) return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
