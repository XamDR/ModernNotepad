using ModernNotepadLibrary.Core;
using ModernNotepadLibrary.Helpers;
using ModernNotepadLibrary.Services;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ModernNotepadLibrary.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private bool closing = false; //we need to use this variable because ContentDialog.ShowAsync() is an async method.

        public MainViewModel()
        {
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

        private bool isInPreviewMode;

        public bool IsInPreviewMode
        {
            get => isInPreviewMode;
            set => Set(ref isInPreviewMode, value);
        }

        private double scale = 1.0;

        public double Scale
        {
            get => scale;
            set => Set(ref scale, value);
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

        public ICommand ClosingWindowCommand => new DelegateCommand<CancelEventArgs>(CloseWindow);

        public ICommand CloseWindowCommand => new DelegateCommand(CloseWindow);

        public ICommand OpenNewWindowCommand => new DelegateCommand(OpenNewWindow);

        public ICommand PrintCommand => new DelegateCommand(Print);

        public ICommand SetZoomFactorCommand => new DelegateCommand<double>(SetZoomFactor);

        public ICommand ShowAboutWindowCommand => new DelegateCommand(ShowAboutWindow);

        public ICommand ShowFindReplaceWindowCommand => new DelegateCommand(ShowFindReplaceWindow);

        public ICommand ShowFontSettingsWindowCommand => new DelegateCommand(ShowFontSettingsWindow);

        public ICommand ShowPrintPreviewCommand => new DelegateCommand(ShowPrintPreview);

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

        private void SetZoomFactor(double parameter) => Scale = parameter != 0.0 ? Scale + parameter : 1.0;

        private void Print() => PrintService.PrintDocument();

        private void ShowAboutWindow() => WindowService.ShowDialog(AboutViewModel, typeof(AboutViewModel));

        private void ShowFindReplaceWindow() => WindowService.Show(FindReplaceViewModel, typeof(FindReplaceViewModel));

        private void ShowFontSettingsWindow() 
            => WindowService.ShowDialog(FontSettingsViewModel, typeof(FontSettingsViewModel));

        private void ShowPrintPreview() => IsInPreviewMode = true;

        private void ShowSettingsWindow() => WindowService.ShowDialog(SettingsViewModel, typeof(SettingsViewModel));
    }
}
