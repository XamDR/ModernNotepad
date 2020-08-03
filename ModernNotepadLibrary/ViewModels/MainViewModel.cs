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
        private bool closing = false; //we need to use this variable because ContentDialog.ShowAsync() is an async method.

        public MainViewModel
        (
            IAdornerService adornerService,
            IContentDialogService dialogService,
            ILocaleManager localeManager,
            IOpenFileService openFileService,
            IPrintService printService,
            ISaveFileService saveFileService,
            ISettingsManager<UserSettings> settingsManager,
            IWindowService windowService,
            IApplicationThemeManager themeManager
        )
        {
            AdornerService = adornerService;
            DialogService = dialogService;
            LocaleManager = localeManager;
            OpenFileService = openFileService;
            PrintService = printService;
            SaveFileService = saveFileService;
            SettingsManager = settingsManager;
            WindowService = windowService;
            ThemeManager = themeManager;
            TextEditor = new TextEditor(this);            
            AboutViewModel = new AboutViewModel(this);
            FindReplaceViewModel = new FindReplaceViewModel(this);
            FontSettingsViewModel = new FontSettingsViewModel(this);
            SettingsViewModel = new SettingsViewModel(this);
        }

        public AboutViewModel AboutViewModel { get; }

        public FindReplaceViewModel FindReplaceViewModel { get; }

        public FontSettingsViewModel FontSettingsViewModel { get; }
        
        public SettingsViewModel SettingsViewModel { get; }

        public TextEditor TextEditor { get; }

        public IAdornerService AdornerService { get; }

        public IContentDialogService DialogService { get; }

        public ILocaleManager LocaleManager { get; }

        public IOpenFileService OpenFileService { get; }

        public IPrintService PrintService { get; }

        public ISaveFileService SaveFileService { get; }

        public ISettingsManager<UserSettings> SettingsManager { get; }

        public IApplicationThemeManager ThemeManager { get; }

        public IWindowService WindowService { get; }

        public string filePath;

        public string FilePath
        {
            get => filePath;
            set => Set(ref filePath, value);
        }

        private bool noTextFound;

        public bool NoTextFound
        {
            get => noTextFound;
            set => Set(ref noTextFound, value);
        }

        private string title;

        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }

        public ICommand ClosingWindowCommand => new DelegateCommand<CancelEventArgs>(CloseWindow);

        public ICommand CloseWindowCommand => new DelegateCommand(CloseWindow);

        public ICommand OpenNewWindowCommand => new DelegateCommand(OpenNewWindow);

        public ICommand ShowAboutWindowCommand => new DelegateCommand(ShowAboutWindow);

        public ICommand ShowFindReplaceWindowCommand => new DelegateCommand(ShowFindReplaceWindow);

        public ICommand ShowFontSettingsWindowCommand => new DelegateCommand(ShowFontSettingsWindow);

        public ICommand ShowSettingsWindowCommand => new DelegateCommand(ShowSettingsWindow);        

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
            SettingsManager.SaveSettings(SettingsViewModel.UserSettings);
        }

        private void OpenNewWindow()
        {
            var psi = new ProcessStartInfo
            {
                FileName = Process.GetCurrentProcess().MainModule.FileName,
            };
            Process.Start(psi);
        }

        private void ShowAboutWindow() => WindowService.ShowDialog(AboutViewModel, typeof(AboutViewModel));

        private void ShowFindReplaceWindow() => WindowService.Show(FindReplaceViewModel, typeof(FindReplaceViewModel));

        private void ShowFontSettingsWindow() 
            => WindowService.ShowDialog(FontSettingsViewModel, typeof(FontSettingsViewModel));

        private void ShowSettingsWindow() => WindowService.ShowDialog(SettingsViewModel, typeof(SettingsViewModel));
    }
}
