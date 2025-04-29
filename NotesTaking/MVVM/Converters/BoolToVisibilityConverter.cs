using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NotesTaking.MVVM.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                bool inverse = false;
                if (parameter != null && parameter is string)
                {
                    string param = (string)parameter;
                    if (param.Equals("Inverse", StringComparison.OrdinalIgnoreCase))
                    {
                        inverse = true;
                    }
                }

                if (inverse)
                {
                    return boolValue ? Visibility.Collapsed : Visibility.Visible;
                }
                else
                {
                    return boolValue ? Visibility.Visible : Visibility.Collapsed;
                }
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
