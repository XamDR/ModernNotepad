using ModernNotepadLibrary.Helpers;
using System.Windows.Input;

namespace ModernNotepadLibrary.ViewModels
{
    public class FontSettingsWindowViewModel : BaseViewModel
    {
        private readonly MainWindowViewModel mainViewModel;

        public FontSettingsWindowViewModel(MainWindowViewModel mainViewModel) => this.mainViewModel = mainViewModel;

        private string selectedFontName = "Segoe UI";

        public string SelectedFontName
        {
            get => selectedFontName;
            set => Set(ref selectedFontName, value);
        }

        private double selectedFontSize = 14.0;

        public double SelectedFontSize
        {
            get => selectedFontSize;
            set => Set(ref selectedFontSize, value);
        }

        public ICommand ChangeFontCommand => new DelegateCommand(ChangeFont);

        public ICommand CloseWindowCommand => new DelegateCommand(CloseWindow);

        private void ChangeFont()
        {
            mainViewModel.TextEditor.TextArea.SetFontFamily(SelectedFontName);
            mainViewModel.TextEditor.TextArea.FontSize = SelectedFontSize;
            mainViewModel.WindowService.Close(GetType());
        }

        private void CloseWindow() => mainViewModel.WindowService.Close(GetType());
    }
}
