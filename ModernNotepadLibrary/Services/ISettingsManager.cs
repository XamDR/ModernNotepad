namespace ModernNotepadLibrary.Services
{
    public interface ISettingsManager<T> where T : class
    {
        string SettingsDirectoryPath { get; }
        T LoadSettings();
        void SaveSettings(T settings);
    }
}
