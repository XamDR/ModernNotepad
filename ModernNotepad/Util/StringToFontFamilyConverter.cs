using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ModernNotepad.Util
{
    class StringToFontFamilyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var fontFamilyName = value.ToString();
                return new FontFamily(fontFamilyName);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var fontFamily = value as FontFamily;
                return fontFamily.Source;
            }
            return null;
        }
    }
}
