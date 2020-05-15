using System;
using System.Globalization;
using System.Windows;

namespace ModernNotepad.Converters
{
    class BooleanToTextWrappingConverter : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var isTextWrappingEnabled = (bool)value;
                return isTextWrappingEnabled ? TextWrapping.Wrap : TextWrapping.NoWrap;
            }
            return TextWrapping.NoWrap;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var textWrapping = (TextWrapping)value;
                return textWrapping == TextWrapping.Wrap;
            }
            return false;
        }
    }
}
