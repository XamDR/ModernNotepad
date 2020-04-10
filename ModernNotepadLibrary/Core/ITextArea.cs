namespace ModernNotepadLibrary.Core
{
    public interface ITextArea
    {
        int CaretIndex { get; set; }
        double FontSize { get; set; }
        string SelectedText { get; set; }
        string Text { get; set; }
        void Clear();
        bool Focus();             
        void Select(int start, int length);        
        void SetFontFamily(string fontFamilyName);        
    }
}
