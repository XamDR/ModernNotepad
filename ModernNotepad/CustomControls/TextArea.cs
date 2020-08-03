using ModernNotepad.Util;
using ModernNotepadLibrary.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ModernNotepad.CustomControls
{
    /// <summary>
    /// Represents a multiline TextBox.
    /// </summary>
    public class TextArea : TextBox, ITextArea
    {
        public TextArea()
        {
            AcceptsReturn = true;
            AcceptsTab = true;
            Loaded += TextArea_Loaded;
        }

        public HighlightCurrentLineAdorner HighlightCurrentLineAdorner { get; private set; }

        public void SetFontFamily(string fontFamilyName) => FontFamily = new FontFamily(fontFamilyName);

        private void TextArea_Loaded(object sender, RoutedEventArgs e)
        {
            HighlightCurrentLineAdorner = new HighlightCurrentLineAdorner(this);
        }
    }
}
