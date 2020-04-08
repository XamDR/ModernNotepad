using ModernNotepadLibrary.Helpers;
using System.Windows.Input;

namespace ModernNotepadLibrary.ViewModels
{
    public class FindReplaceWindowViewModel : BaseViewModel
    {
        private readonly MainWindowViewModel mainViewModel;

        public FindReplaceWindowViewModel(MainWindowViewModel mainViewModel) => this.mainViewModel = mainViewModel;

        private bool isCaseEnabled;

        public bool IsCaseEnabled
        {
            get => isCaseEnabled;

            set => Set(ref isCaseEnabled, value);
        }

        private string textToReplace;

        public string TextToReplace
        {
            get => textToReplace;
            set => Set(ref textToReplace, value);            
        }

        private string textToSearch;

        public string TextToSearch
        {
            get => textToSearch;
            set => Set(ref textToSearch, value);            
        }

        public ICommand CloseWindowCommand => new DelegateCommand(CloseWindow);

        public ICommand FindTextCommand => new DelegateCommand(FindText);

        public ICommand ReplaceAllTextCommand => new DelegateCommand(ReplaceAllText);

        public ICommand ReplaceTextCommand => new DelegateCommand(ReplaceText);

        private async void FindText()
        {
            //var text = mainViewModel.TextEditor.TextArea.Text;            

            if (mainViewModel.TextEditor.TextArea.Text.Contains(TextToSearch))
            {
                var text = mainViewModel.TextEditor.TextArea.Text;
                mainViewModel.TextEditor.TextArea.Focus();
                mainViewModel.TextEditor.TextArea.Select(text.IndexOf(TextToSearch), TextToSearch.Length);
                //mainViewModel.TextEditor.TextArea.Text = text.Substring(text.IndexOf(TextToSearch) + TextToSearch.Length);
                //text = text.Remove(0, text.IndexOf(TextToSearch) + TextToSearch.Length);
            }
            else
            {
                var message = $"{mainViewModel.LocaleManager.LoadString("NoFindResults")} {TextToSearch}";
                await mainViewModel.DialogService.ShowInformationAsync(message);
                return;
            }
        }

        private void ReplaceAllText()
        {
            
        }

        private void ReplaceText()
        {
            
        }

        private void CloseWindow() => mainViewModel.WindowService.Close(GetType());
    }
}
