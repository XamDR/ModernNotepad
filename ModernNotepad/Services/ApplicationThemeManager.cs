using ModernNotepadLibrary.Services;
using ModernWpf;

namespace ModernNotepad.Services
{
    class ApplicationThemeManager : IApplicationThemeManager
    {
        public void ChangeTheme(bool? isDarkThemeRequested)
        {
            ThemeManager.Current.ApplicationTheme = isDarkThemeRequested switch
            {
                true => ApplicationTheme.Dark,
                false => ApplicationTheme.Light,
                _ => null,
            };            
        }
    }
}
