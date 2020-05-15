using ModernNotepadLibrary.Services;
using ModernWpf;

namespace ModernNotepad.Services
{
    class ApplicationThemeManager : IApplicationThemeManager
    {
        public void ChangeTheme(bool isDarkThemeRequested) 
            => ThemeManager.Current.ApplicationTheme = isDarkThemeRequested ? ApplicationTheme.Dark : ApplicationTheme.Light;
    }
}
