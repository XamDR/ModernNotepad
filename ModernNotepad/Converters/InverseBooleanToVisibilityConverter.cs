using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace ModernNotepad.Converters
{
    class InverseBooleanToVisibilityConverter : BaseConverter
    {
        private readonly BooleanToVisibilityConverter converter = new BooleanToVisibilityConverter();

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = converter.Convert(value, targetType, parameter, culture) as Visibility?;

            return result == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = converter.ConvertBack(value, targetType, parameter, culture) as bool?;

            return result == true ? false : true;
        }
    }
}
