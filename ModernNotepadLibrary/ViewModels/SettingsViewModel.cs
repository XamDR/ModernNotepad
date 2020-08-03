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

        public bool? IsDarkThemeEnabled
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

        private bool isHighlightAdornerVisible;

        public bool IsHighlightAdornerVisible
        {
            get => isHighlightAdornerVisible;
            set => Set(ref isHighlightAdornerVisible, value);
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

        public ICommand ChangeThemeCommand => new DelegateCommand(ChangeTheme);

        public ICommand HighlightCurrentLineCommand => new DelegateCommand(HighlightCurrentLine);

        private void ChangeTheme() => mainViewModel.ThemeManager.ChangeTheme(IsDarkThemeEnabled);

        private void HighlightCurrentLine()
        {
            if (IsHighlightAdornerVisible)
            {
                mainViewModel.AdornerService.AddAdorner(mainViewModel.TextEditor.TextArea);
            }
            else
            {
                mainViewModel.AdornerService.RemoveAdorner(mainViewModel.TextEditor.TextArea);
            }
        }
    }
}
