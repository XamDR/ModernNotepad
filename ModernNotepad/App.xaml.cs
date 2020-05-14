using ModernNotepadLibrary.Core;
using ModernNotepadLibrary.Services;
using ModernNotepadLibrary.ViewModels;
using ModernWpf;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;

namespace ModernNotepad
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainViewModel = CreateMainViewModel();
            var mainWindow = mainViewModel.WindowService.ShowMainWindow(mainViewModel, typeof(MainWindowViewModel));
            mainViewModel.TextEditor.TextArea = mainWindow.TextArea;

            if (!Directory.Exists(mainViewModel.SettingsManager.SettingsDirectoryPath))
            {
                mainViewModel.SettingsManager.SaveSettings(mainViewModel.UserSettings);
            }
            ApplySettings(mainViewModel);
            ThemeManager.Current.ApplicationTheme = mainViewModel.IsDarkThemeEnabled 
                                                    ? ApplicationTheme.Dark : ApplicationTheme.Light;            
            LoadLocale(mainViewModel);
            mainViewModel.Title = mainViewModel.LocaleManager.LoadString("AppTitle");
            mainViewModel.FilePath = mainViewModel.LocaleManager.LoadString("NewDocument");

            if (e.Args.Length > 0)
            {
                mainViewModel.Title = Path.GetFileName(e.Args[0]);
                mainViewModel.FilePath = Path.GetFullPath(e.Args[0]);
                mainViewModel.TextEditor.TextArea.Text = File.ReadAllText(e.Args[0], Encoding.Default);
                mainViewModel.TextEditor.SavedAsFile = true;
                mainViewModel.SaveFileService.FileName = e.Args[0];
            }
        }

        private MainWindowViewModel CreateMainViewModel()
        {
            var mainViewModel = new MainWindowViewModel
            {
                DialogService = Program.ServiceResolver.Create<IContentDialogService>(),
                WindowService = Program.ServiceResolver.Create<IWindowService>(),
                OpenFileService = Program.ServiceResolver.Create<IOpenFileService>(),
                SaveFileService = Program.ServiceResolver.Create<ISaveFileService>(),
                ThemeManager = Program.ServiceResolver.Create<IApplicationThemeManager>(),
                SettingsManager = Program.ServiceResolver.Create<ISettingsManager<UserSettings>>(),
                LocaleManager = Program.ServiceResolver.Create<ILocaleManager>(),
                PrintService = Program.ServiceResolver.Create<IPrintService>(),
            };
            return mainViewModel;
        }

        private void ApplySettings(MainWindowViewModel mainViewModel)
        {
            mainViewModel.IsDarkThemeEnabled = mainViewModel.SettingsManager.LoadSettings().IsDarkThemeEnabled;
            mainViewModel.IsSpellCheckingEnabled = mainViewModel.SettingsManager.LoadSettings().IsSpellCheckingEnabled;
            mainViewModel.IsStatusBarVisible = mainViewModel.SettingsManager.LoadSettings().IsStatusBarVisible;
            mainViewModel.IsWordWrapEnabled = mainViewModel.SettingsManager.LoadSettings().IsWordWrapEnabled;
        }

        private void LoadLocale(MainWindowViewModel mainViewModel)
        {
            try
            {                
                mainViewModel.LocaleManager.LoadStringResource(CultureInfo.CurrentUICulture.Name);
            }
            catch (Exception)
            {
                mainViewModel.LocaleManager.LoadStringResource("en-US");
            }
        }
    }
}
