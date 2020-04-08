using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace ModernNotepad.Util
{
    class FontWrapper
    {
        public IEnumerable<FontFamily> FontFamilies => Fonts.SystemFontFamilies.OrderBy(f => f.Source);

        public IEnumerable<double> FontSizes => new List<double> { 8.0, 9.0, 10.0, 11.0, 12.0, 14.0, 16.0, 18.0, 20.0, 
                                                                   24.0, 28.0, 32.0, 36.0, 40.0, 48.0, 56.0, 64.0, 72.0 };
    }
}
