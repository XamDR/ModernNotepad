using System;
using System.Globalization;

namespace ModernNotepad.Converters
{
    class StringToZoomFactorConverter : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
            => (value.ToString()) switch
            {
                "ZoomIn" => 0.1,
                "ZoomOut" => -0.1,
                "Zoom" => 0.0,
                _ => 0.0,
            };
    }
}
