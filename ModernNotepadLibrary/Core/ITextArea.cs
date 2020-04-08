namespace ModernNotepadLibrary.Core
{
    public interface ITextArea
    {
        double FontSize { get; set; }
        int SelectionLength { get; set; }
        int SelectionStart { get; set; }
        string Text { get; set; }
        void Clear();
        bool Focus();             
        void Select(int start, int length);        
        void SetFontFamily(string fontFamilyName);        
    }
}
