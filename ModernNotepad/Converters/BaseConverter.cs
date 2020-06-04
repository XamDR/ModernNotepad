using System;
using System.Globalization;
using System.Windows.Data;

namespace ModernNotepad.Converters
{
    class BaseConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => Binding.DoNothing;

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => Binding.DoNothing;
    }
}
