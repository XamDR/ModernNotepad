namespace ModernNotepadLibrary.Services
{
    public interface ILocaleManager
    {
        bool IsLocalAvailable { get; }
        string LoadString(string key);
        void LoadStringResource(string locale);
    }
}
