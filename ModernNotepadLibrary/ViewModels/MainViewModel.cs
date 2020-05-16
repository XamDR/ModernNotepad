using ModernNotepadLibrary.Core;
using ModernNotepadLibrary.Helpers;
using ModernNotepadLibrary.Services;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ModernNotepadLibrary.ViewModels
{
    public class MainViewModel : BaseViewModel
    {        
        private readonly AboutViewModel aboutWindow;
        private readonly FontSettingsViewModel fontSettings;
        private bool closing = false; //we need to use this variable because ContentDialog.ShowAsync() is an async method.

        public MainViewModel()
        {
            TextEditor = new TextEditor(this);
            UserSettings = new UserSettings();
            aboutWindow = new AboutViewModel(this);
            FindReplace = new FindReplaceViewModel(this);
            fontSettings = new FontSettingsViewModel(this);
        }

        public FindReplaceViewModel FindReplace { get; }

        public TextEditor TextEditor { get; }

        public UserSettings UserSettings { get; }

        public IContentDialogService DialogService { get; set; }

        public ILocaleManager LocaleManager { get; set; }

        public IOpenFileService OpenFileService { get; set; }

        public IPrintService PrintService { get; set; }

        public ISaveFileService SaveFileService { get; set; }

        public ISettingsManager<UserSettings> SettingsManager { get; set; }

        public string filePath;

        public string FilePath
        {
            get => filePath;
            set => Set(ref filePath, value);
        }

        public bool IsDarkThemeEnabled
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

        private bool isInPreviewMode;

        public bool IsInPreviewMode
        {
            get => isInPreviewMode;
            set => Set(ref isInPreviewMode, value);
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

        private bool shouldPopupBeOpen;

        public bool ShouldPopupBeOpen
        {
            get => shouldPopupBeOpen;
            set => Set(ref shouldPopupBeOpen, value);
        }

        private string title;

        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }

        public IWindowService WindowService { get; set; }

        public IApplicationThemeManager ThemeManager { get; set; }

        public ICommand ClosePrintPreviewCommand => new DelegateCommand(ClosePrintPreview);        

        public ICommand ClosingWindowCommand => new DelegateCommand<CancelEventArgs>(CloseWindow);

        public ICommand CloseWindowCommand => new DelegateCommand(CloseWindow);

        public ICommand OpenNewWindowCommand => new DelegateCommand(OpenNewWindow);

        public ICommand PrintCommand => new DelegateCommand(Print);        

        public ICommand ShowAboutWindowCommand => new DelegateCommand(ShowAboutWindow);

        public ICommand ShowFindReplaceWindowCommand => new DelegateCommand(ShowFindReplaceWindow);

        public ICommand ShowFontSettingsWindowCommand => new DelegateCommand(ShowFontSettingsWindow);

        public ICommand ShowPrintPreviewCommand => new DelegateCommand(ShowPrintPreview);        

        public ICommand ToggleThemeCommand => new DelegateCommand<bool>(ToggleTheme);

        private void ClosePrintPreview() => IsInPreviewMode = false;

        private void CloseWindow() => WindowService.CloseMainWindow();

        private async void CloseWindow(CancelEventArgs e)
        {            
            if ((TextEditor.SavedAsFile || !string.IsNullOrEmpty(TextEditor.TextArea.Text)) && TextEditor.UnsavedChanges)
            {
                if (!closing)
                {
                    e.Cancel = true;
                    var answer = await DialogService.AskConfirmationAsync(LocaleManager.LoadString("ConfirmationQuestion"));

                    switch (answer)
                    {
                        case true:
                            TextEditor.SaveFileCommand.Execute(null);
                            closing = true;
                            WindowService.CloseMainWindow();
                            break;
                        case false: 
                            closing = true; 
                            WindowService.CloseMainWindow();
                            break;
                        case null: return;
                    }
                }
            }
            SettingsManager.SaveSettings(UserSettings);
        }

        private void OpenNewWindow()
        {
            var psi = new ProcessStartInfo
            {
                FileName = Process.GetCurrentProcess().MainModule.FileName,
            };
            Process.Start(psi);
        }

        private void Print() => PrintService.PrintDocument();

        private void ShowAboutWindow() => WindowService.ShowDialog(aboutWindow, typeof(AboutViewModel));

        private void ShowFindReplaceWindow() => WindowService.Show(FindReplace, typeof(FindReplaceViewModel));

        private void ShowFontSettingsWindow() 
            => WindowService.ShowDialog(fontSettings, typeof(FontSettingsViewModel));

        private void ShowPrintPreview() => IsInPreviewMode = true;

        private void ToggleTheme(bool isDarkThemeRequested) => ThemeManager.ChangeTheme(isDarkThemeRequested);
    }
}
