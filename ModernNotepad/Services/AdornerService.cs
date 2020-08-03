using ModernNotepad.CustomControls;
using ModernNotepadLibrary.Core;
using ModernNotepadLibrary.Services;
using System.Windows.Documents;

namespace ModernNotepad.Services
{
    class AdornerService : IAdornerService
    {
        public void AddAdorner(ITextArea textArea)
        {
            var adornedTextArea = textArea as TextArea;
            var adornerLayer = AdornerLayer.GetAdornerLayer(adornedTextArea);

            if (adornerLayer.GetAdorners(adornedTextArea) != null) // Workaround
            {
                adornerLayer.Remove(adornedTextArea.HighlightCurrentLineAdorner);
            }
            adornerLayer.Add(adornedTextArea.HighlightCurrentLineAdorner);
        }

        public void RemoveAdorner(ITextArea textArea)
        {
            var adornedTextArea = textArea as TextArea;
            var adornerLayer = AdornerLayer.GetAdornerLayer(adornedTextArea);
            adornerLayer.Remove(adornedTextArea.HighlightCurrentLineAdorner);
        }
    }
}
