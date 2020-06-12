using ModernNotepadLibrary.Core;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

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

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.A)
            {
                e.Handled = true;
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.E)
            {
                SelectAll();
            }
        }
    }
}
