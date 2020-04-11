using ModernNotepadLibrary.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModernNotepadLibrary.ViewModels
{
    public class FindReplaceWindowViewModel : BaseViewModel
    {
        private readonly MainWindowViewModel mainViewModel;
        private int index = 0;

        public FindReplaceWindowViewModel(MainWindowViewModel mainViewModel) => this.mainViewModel = mainViewModel;

        private bool matchCase;

        public bool MatchCase
        {
            get => matchCase;
            set => Set(ref matchCase, value);
        }

        private bool textNotFound;
        
        public bool TextNotFound
        {
            get => textNotFound;
            set => Set(ref textNotFound, value);
        }

        private string textToFind = string.Empty;

        public string TextToFind
        {
            get => textToFind;
            set => Set(ref textToFind, value);
        }

        private string textToReplace = string.Empty;

        public string TextToReplace
        {
            get => textToReplace;
            set => Set(ref textToReplace, value);            
        }

        public ICommand FindNextCommand => new DelegateCommand(FindNext);

        public ICommand FindPreviousCommand => new DelegateCommand(FindPrevious);

        public ICommand ReplaceAllTextCommand => new DelegateCommand(ReplaceAllText);

        public ICommand ReplaceTextCommand => new DelegateCommand(ReplaceText);

        private void FindNext()
        {
            var textArea = mainViewModel.TextEditor.TextArea;
            var text = textArea.Text;
            var indexes = MatchCase ? text.AllIndexesOf(TextToFind, StringComparison.Ordinal).ToList() 
                                    : text.AllIndexesOf(TextToFind).ToList();            

            var result = MatchCase ? text.Contains(TextToFind, StringComparison.Ordinal)
                                   : text.Contains(TextToFind, StringComparison.OrdinalIgnoreCase);
            if (result)
            {   
                if (index >= indexes.Count)
                {
                    index = 0;
                }
                textArea.Focus();
                
                if (MatchCase)
                {
                    textArea.Select(text.IndexOf(TextToFind, indexes[index], StringComparison.Ordinal), TextToFind.Length);
                }
                else
                {
                    textArea.Select(text.IndexOf(TextToFind, indexes[index], StringComparison.OrdinalIgnoreCase), TextToFind.Length);
                }
                index++;
            }
            else
            {
                TextNotFound = true;
            }
        }

        private void FindPrevious()
        {
            var textArea = mainViewModel.TextEditor.TextArea;
            var text = textArea.Text;
            var indexes = MatchCase ? text.AllIndexesOf(TextToFind, StringComparison.Ordinal).OrderByDescending(i => i).ToList() 
                                    : text.AllIndexesOf(TextToFind).OrderByDescending(i => i).ToList();            

            var result = MatchCase ? text.Contains(TextToFind, StringComparison.Ordinal)
                                   : text.Contains(TextToFind, StringComparison.OrdinalIgnoreCase);
            if (result)
            {
                if (index >= indexes.Count)
                {
                    index = 0;
                }
                textArea.Focus();

                if (MatchCase)
                {
                    textArea.Select(text.IndexOf(TextToFind, indexes[index], StringComparison.Ordinal), TextToFind.Length);
                }
                else
                {
                    textArea.Select(text.IndexOf(TextToFind, indexes[index], StringComparison.OrdinalIgnoreCase), TextToFind.Length);
                }
                index++;
            }
            else
            {
                TextNotFound = true;
            }
        }

        private void ReplaceAllText()
        {
            var text = mainViewModel.TextEditor.TextArea.Text;

            var newText = MatchCase ? text.Replace(TextToFind, TextToReplace, StringComparison.Ordinal)
                                    : text.Replace(TextToFind, TextToReplace, StringComparison.OrdinalIgnoreCase);

            mainViewModel.TextEditor.TextArea.Text = newText;
        }
        
        private void ReplaceText()
        {
            var textArea = mainViewModel.TextEditor.TextArea;
            var text = textArea.Text;
            var indexes = MatchCase ? text.AllIndexesOf(TextToFind, StringComparison.Ordinal) : text.AllIndexesOf(TextToFind);
            
            var result = MatchCase ? text.Contains(TextToFind, StringComparison.Ordinal)
                                   : text.Contains(TextToFind, StringComparison.OrdinalIgnoreCase);
            if (result)
            {
                var bestIndex = indexes.FirstOrDefault(i => i >= textArea.CaretIndex);
                textArea.Focus();

                if (MatchCase)
                {
                    textArea.Select(text.IndexOf(TextToFind, bestIndex, StringComparison.Ordinal), TextToFind.Length);
                }
                else
                {
                    textArea.Select(text.IndexOf(TextToFind, bestIndex, StringComparison.OrdinalIgnoreCase), TextToFind.Length);
                }
                textArea.SelectedText = TextToReplace;
            }
            else
            {
                TextNotFound = true;
            }
        }
    }
}
