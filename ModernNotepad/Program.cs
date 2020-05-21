using ModernNotepad.Dialogs;
using ModernNotepad.Services;
using ModernNotepadLibrary.Core;
using ModernNotepadLibrary.Helpers;
using ModernNotepadLibrary.Services;
using System;
using System.Windows;

namespace ModernNotepad
{
    class Program
    {
        public static ServiceResolver ServiceResolver { get; private set; }

        [STAThread]
        public static void Main()
        {
            ShowSplashScreen();           
            var app = new App();
            app.InitializeComponent();            
            RegisterServices();
            app.Run();
        }

        private static void ShowSplashScreen()
        {
            var splashScreen = new SplashScreen("Images/SplashScreen.png");
            splashScreen.Show(true);
        }

        private static void RegisterServices()
        {
            ServiceResolver = new ServiceResolver();
            ServiceResolver.Register<IApplicationThemeManager, ApplicationThemeManager>();
            ServiceResolver.Register<IContentDialogService, ContentDialogService>();
            ServiceResolver.Register<ILocaleManager, LocaleManager>();
            ServiceResolver.Register<IOpenFileService, OpenFileDialogService>();
            ServiceResolver.Register<IPrintService, PrintService>();
            ServiceResolver.Register<ISaveFileService, SaveFileDialogService>();
            ServiceResolver.Register<ISettingsManager<UserSettings>, SettingsManager>();
            ServiceResolver.Register<IWindowService, WindowService>();            
        }
    }
}
