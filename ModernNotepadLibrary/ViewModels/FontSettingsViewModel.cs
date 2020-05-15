using ModernNotepadLibrary.Helpers;
using System.Windows.Input;

namespace ModernNotepadLibrary.ViewModels
{
    public class FontSettingsViewModel : BaseViewModel
    {
        private readonly MainViewModel mainViewModel;

        public FontSettingsViewModel(MainViewModel mainViewModel) => this.mainViewModel = mainViewModel;

        private string selectedFontName = "Consolas";

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
