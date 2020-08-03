using ModernNotepadLibrary.Core;

namespace ModernNotepadLibrary.Services
{
    public interface IAdornerService
    {
        void AddAdorner(ITextArea textArea);
        void RemoveAdorner(ITextArea textArea);
    }
}
