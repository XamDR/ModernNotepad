using ModernNotepadLibrary.Core;
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
        }

        public void SetFontFamily(string fontFamilyName) => FontFamily = new FontFamily(fontFamilyName);        
    }
}
