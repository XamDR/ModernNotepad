using ModernWpf;
using System;
using System.Globalization;

namespace ModernNotepad.Converters
{
    class BooleanToSelectedIndexConverter : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
            => ((bool?)value) switch
            {
                false => (int)ApplicationTheme.Light,
                true => (int)ApplicationTheme.Dark,
                null => Enum.GetValues(typeof(ApplicationTheme)).Length,
            };

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
            => ((int)value) switch
            {
                (int)ApplicationTheme.Light => false,
                (int)ApplicationTheme.Dark => true,
                _ => null,
            };
    }
}
