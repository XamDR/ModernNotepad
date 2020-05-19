using ModernNotepadLibrary.Core;
using ModernNotepadLibrary.Helpers;
using System.Windows.Input;

namespace ModernNotepadLibrary.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly MainViewModel mainViewModel;

        public SettingsViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            UserSettings = new UserSettings();
        }

        public UserSettings UserSettings { get; }

        public bool IsDarkThemeRequested
        {
            get => UserSettings.IsDarkThemeEnabled;
            set
            {
                if (UserSettings.IsDarkThemeEnabled != value)
                {
                    UserSettings.IsDarkThemeEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsSpellCheckingEnabled
        {
            get => UserSettings.IsSpellCheckingEnabled;
            set
            {
                if (UserSettings.IsSpellCheckingEnabled != value)
                {
                    UserSettings.IsSpellCheckingEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsStatusBarVisible
        {
            get => UserSettings.IsStatusBarVisible;
            set
            {
                if (UserSettings.IsStatusBarVisible != value)
                {
                    UserSettings.IsStatusBarVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsWordWrapEnabled
        {
            get => UserSettings.IsWordWrapEnabled;
            set
            {
                if (UserSettings.IsWordWrapEnabled != value)
                {
                    UserSettings.IsWordWrapEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ToggleThemeCommand => new DelegateCommand<bool>(ToggleTheme);

        private void ToggleTheme(bool isDarkThemeRequested) => mainViewModel.ThemeManager.ChangeTheme(isDarkThemeRequested);
    }
}
