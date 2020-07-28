using ModernNotepadLibrary.ViewModels;
using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace ModernNotepad
{
    public partial class App : Application
    {
        public MainViewModel MainViewModel { get; }

        public App() => MainViewModel = (MainViewModel)Program.ServiceResolver.Create<INotifyPropertyChanged>();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);            
            var mainWindow = MainViewModel.WindowService.CreateMainView(MainViewModel, typeof(MainViewModel));
            MainViewModel.TextEditor.TextArea = mainWindow.TextArea;            

            if (!Directory.Exists(MainViewModel.SettingsManager.SettingsDirectoryPath))
            {
                MainViewModel.SettingsManager.SaveSettings(MainViewModel.SettingsViewModel.UserSettings);
            }
            ApplySettings(MainViewModel);
            LoadLocale(MainViewModel);
            MainViewModel.Title = MainViewModel.LocaleManager.LoadString("AppTitle");
            MainViewModel.FilePath = MainViewModel.LocaleManager.LoadString("NewDocument");

            if (e.Args.Length > 0)
            {
                MainViewModel.TextEditor.SavedAsFile = true;
                MainViewModel.Title = Path.GetFileName(e.Args[0]);
                MainViewModel.FilePath = Path.GetFullPath(e.Args[0]);
                MainViewModel.TextEditor.TextArea.Text = File.ReadAllText(e.Args[0], Encoding.Default);
                //MainViewModel.TextEditor.SavedAsFile = true;
                MainViewModel.SaveFileService.FileName = e.Args[0];
            }
        }

        private void ApplySettings(MainViewModel MainViewModel)
        {
            MainViewModel.SettingsViewModel.IsDarkThemeEnabled = MainViewModel.SettingsManager.LoadSettings().IsDarkThemeEnabled;
            MainViewModel.SettingsViewModel.IsSpellCheckingEnabled = MainViewModel.SettingsManager.LoadSettings().IsSpellCheckingEnabled;
            MainViewModel.SettingsViewModel.IsStatusBarVisible = MainViewModel.SettingsManager.LoadSettings().IsStatusBarVisible;
            MainViewModel.SettingsViewModel.IsWordWrapEnabled = MainViewModel.SettingsManager.LoadSettings().IsWordWrapEnabled;
            MainViewModel.ThemeManager.ChangeTheme(MainViewModel.SettingsViewModel.IsDarkThemeEnabled);
        }

        private void LoadLocale(MainViewModel MainViewModel)
        {
            try
            {
                MainViewModel.LocaleManager.LoadStringResource(CultureInfo.CurrentUICulture.Name);                
            }
            catch (Exception)
            {
                MainViewModel.LocaleManager.LoadStringResource("en-US");
            }
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {            
            WriteLogFile(e.Exception.ToString());
            const long MB_OK = 0x0L, MB_ERROR = 0x10L;
            var message = MainViewModel.LocaleManager.LoadString("ErrorMessage");            
            MessageBox(IntPtr.Zero, message, "Modern Notepad", (uint)(MB_OK | MB_ERROR));
            e.Handled = true;
            Current.Shutdown();
        }

        private void WriteLogFile(string message)
        {
            var logPath = @$"{Directory.GetCurrentDirectory()}\error.log";
            File.WriteAllText(logPath, message);
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);
    }
}
