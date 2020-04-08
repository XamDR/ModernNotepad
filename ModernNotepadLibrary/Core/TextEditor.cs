using ModernNotepadLibrary.Helpers;
using ModernNotepadLibrary.ViewModels;
using System.IO;
using System.Text;
using System.Windows.Input;

namespace ModernNotepadLibrary.Core
{
    public class TextEditor
    {
        private readonly MainWindowViewModel mainViewModel;

        public TextEditor(MainWindowViewModel mainViewModel) => this.mainViewModel = mainViewModel;

        public bool SavedAsFile { get; set; } = false;

        public bool UnsavedChanges { get; set; } = false;

        public ITextArea TextArea { get; set; }

        public ICommand CreateNewDocumentCommand => new DelegateCommand(CreateDocument);

        public ICommand OpenFileCommand => new DelegateCommand(OpenFile);
        
        public ICommand SaveFileCommand => new DelegateCommand(SaveFile);

        public ICommand SaveFileAsCommand => new DelegateCommand(SaveFileAs);
        
        private async void CreateDocument()
        {            
            if ((SavedAsFile || !string.IsNullOrEmpty(TextArea.Text)) && UnsavedChanges)
            {
                var result = await mainViewModel.DialogService.AskConfirmationAsync(mainViewModel.LocaleManager.LoadString("ConfirmationQuestion"));

                switch (result)
                {
                    case true: SaveFileAs(); return;
                    case false:
                        TextArea.Clear();
                        mainViewModel.Title = string.Empty;
                        SavedAsFile = false;
                        return;
                    case null: return;
                }
            }
            else
            {
                TextArea.Clear();
                mainViewModel.Title = mainViewModel.LocaleManager.LoadString("AppTitle");
                SavedAsFile = false;
            }
        }

        private void OpenFile()
        {            
            InitializeOpenFileService();

            if (mainViewModel.OpenFileService.ShowDialog() == true && mainViewModel.OpenFileService.FileName.Length > 0)
            {
                SavedAsFile = true;
                TextArea.Text = File.ReadAllText(mainViewModel.OpenFileService.FileName, Encoding.Default);
                mainViewModel.Title = Path.GetFileName(mainViewModel.OpenFileService.FileName);
            }
        }

        private void SaveFile()
        {
            if (!SavedAsFile)
            {
                InitializeSaveFileService();
                if (mainViewModel.SaveFileService.ShowDialog() == true && mainViewModel.SaveFileService.FileName.Length > 0)
                {
                    mainViewModel.Title = Path.GetFileName(mainViewModel.SaveFileService.FileName);
                }
                else
                {
                    return;
                }
            }
            if (File.Exists(mainViewModel.OpenFileService.FileName))
            {
                mainViewModel.SaveFileService.FileName = mainViewModel.OpenFileService.FileName;
            }
            mainViewModel.Title = Path.GetFileName(mainViewModel.SaveFileService.FileName);
            UnsavedChanges = false;
            File.WriteAllText(mainViewModel.SaveFileService.FileName, TextArea.Text, Encoding.Default);
            SavedAsFile = true;
        }

        private void SaveFileAs()
        {
            InitializeSaveFileService();

            if (mainViewModel.SaveFileService.ShowDialog() == true && mainViewModel.SaveFileService.FileName.Length > 0)
            {
                File.WriteAllText(mainViewModel.SaveFileService.FileName, TextArea.Text, Encoding.Default);
                mainViewModel.Title = Path.GetFileName(mainViewModel.SaveFileService.FileName);
            }
        }
        
        private void InitializeOpenFileService()
        {
            mainViewModel.OpenFileService.DefaultExtension = ".txt";
            mainViewModel.OpenFileService.Filter = string.Join("", new string[]
            {
                $"{mainViewModel.LocaleManager.LoadString("FilterTxt")} (*.txt)|*.txt|",
                $"{mainViewModel.LocaleManager.LoadString("FilterAll")} (*.*)|*.*"
            });
            mainViewModel.OpenFileService.FilterIndex = 1;
            mainViewModel.OpenFileService.MultiSelect = false;
            mainViewModel.OpenFileService.Title = mainViewModel.LocaleManager.LoadString("OpenFileTitle");
        }

        private void InitializeSaveFileService()
        {
            mainViewModel.SaveFileService.AddExtension = true;
            mainViewModel.SaveFileService.DefaultExtension = ".txt";
            mainViewModel.SaveFileService.Filter = string.Join("", new string[]
            {
                $"{mainViewModel.LocaleManager.LoadString("FilterTxt")} (*.txt)|*.txt|",
                $"{mainViewModel.LocaleManager.LoadString("FilterAll")} (*.*)|*.*"
            });
            mainViewModel.SaveFileService.FilterIndex = 1;
            mainViewModel.SaveFileService.Title = mainViewModel.LocaleManager.LoadString("SaveFileTitle");
        }
    }
}
