using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfFrontEnd.Converters
{
    public class NullableDateIsNullToVisibilityVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var objectAsNullableDateTime = value as DateTime?;
            if(objectAsNullableDateTime != null) return Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Visibility.Collapsed;
        }
    }
}
