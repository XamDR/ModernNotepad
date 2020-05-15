using System;
using System.Globalization;
using System.Windows.Media;

namespace ModernNotepad.Converters
{
    class StringToFontFamilyConverter : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var fontFamilyName = value.ToString();
                return new FontFamily(fontFamilyName);
            }
            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
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
