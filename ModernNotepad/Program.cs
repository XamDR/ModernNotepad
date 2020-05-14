using ModernNotepad.Dialogs;
using ModernNotepad.Util;
using ModernNotepadLibrary.Core;
using ModernNotepadLibrary.Helpers;
using ModernNotepadLibrary.Services;
using System;

namespace ModernNotepad
{
    class Program
    {
        public static ServiceResolver ServiceResolver { get; private set; }

        [STAThread]
        public static void Main()
        {
            var app = new App();
            app.InitializeComponent();
            ServiceResolver = new ServiceResolver();
            ServiceResolver.Register<IApplicationThemeManager, ApplicationThemeManager>();
            ServiceResolver.Register<IContentDialogService, ContentDialogService>();
            ServiceResolver.Register<ILocaleManager, LocaleManager>();
            ServiceResolver.Register<IOpenFileService, OpenFileDialogService>();
            ServiceResolver.Register<ISaveFileService, SaveFileDialogService>();
            ServiceResolver.Register<ISettingsManager<UserSettings>, SettingsManager>();
            ServiceResolver.Register<IWindowService, WindowService>();
            ServiceResolver.Register<IPrintService, PrintService>();
            app.Run();
        }
    }
}
