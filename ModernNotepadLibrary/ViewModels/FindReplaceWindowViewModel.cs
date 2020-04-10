using ModernNotepadLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        private SearchDirection searchDirection;

        public SearchDirection SearchDirection
        {
            get => SearchDirection;
            set => Set(ref searchDirection, value);
        }

        private string textToFind;

        public string TextToFind
        {
            get => textToFind;
            set => Set(ref textToFind, value);
        }

        private string textToReplace;

        public string TextToReplace
        {
            get => textToReplace;
            set => Set(ref textToReplace, value);            
        }

        public ICommand CloseWindowCommand => new DelegateCommand(CloseWindow);

        public ICommand FindTextCommand => new DelegateCommand(FindNext);

        public ICommand ReplaceAllTextCommand => new DelegateCommand(ReplaceAllText);

        public ICommand ReplaceTextCommand => new DelegateCommand(ReplaceText);

        private void CloseWindow() => mainViewModel.WindowService.Close(GetType());

        private async void FindNext()
        {
            var textArea = mainViewModel.TextEditor.TextArea;
            var text = textArea.Text;

            if (SearchDirection == SearchDirection.Down)
            {
                
            }
            var indexes = MatchCase ? text.AllIndexesOf(TextToFind, StringComparison.Ordinal) : text.AllIndexesOf(TextToFind);
            //var indexes = MatchCase ? Regex.Matches(text, TextToFind).Select(m => m.Index).ToList()
            //                        : Regex.Matches(text, TextToFind, RegexOptions.IgnoreCase).Select(m => m.Index).ToList();

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
                var message = $"{mainViewModel.LocaleManager.LoadString("NoFindResults")} '{TextToFind}'.";
                await mainViewModel.DialogService.ShowInformationAsync(message);
            }
        }

        //private async void FindPrevious()
        //{
        //    var textArea = mainViewModel.TextEditor.TextArea;
        //    var text = textArea.Text;
        //    var indexes = MatchCase ? Regex.Matches(text, TextToFind).Select(m => m.Index)
        //                                                             .OrderByDescending(i => i).ToList()
        //                            : Regex.Matches(text, TextToFind, RegexOptions.IgnoreCase).Select(m => m.Index)
        //                                                                                      .OrderByDescending(i => i).ToList();

        //    var result = MatchCase ? text.Contains(TextToFind, StringComparison.Ordinal)
        //                           : text.Contains(TextToFind, StringComparison.OrdinalIgnoreCase);

        //    if (result)
        //    {
        //        if (index >= indexes.Count)
        //        {
        //            index = 0;
        //        }
        //        textArea.Focus();

        //        if (MatchCase)
        //        {                    
        //            textArea.Select(text.IndexOf(TextToFind, indexes[index], StringComparison.Ordinal), TextToFind.Length);
        //        }
        //        else
        //        {
        //            textArea.Select(text.IndexOf(TextToFind, indexes[index], StringComparison.OrdinalIgnoreCase), TextToFind.Length);
        //        }
        //        index++;
        //    }
        //    else
        //    {
        //        var message = $"{mainViewModel.LocaleManager.LoadString("NoFindResults")} '{TextToFind}'.";
        //        await mainViewModel.DialogService.ShowInformationAsync(message);
        //    }
        //}

        private void ReplaceAllText()
        {
            var text = mainViewModel.TextEditor.TextArea.Text;

            var newText = MatchCase ? text.Replace(TextToFind, TextToReplace, StringComparison.Ordinal)
                                    : text.Replace(TextToFind, TextToReplace, StringComparison.OrdinalIgnoreCase);

            mainViewModel.TextEditor.TextArea.Text = newText;
        }
        
        private async void ReplaceText()
        {
            var textArea = mainViewModel.TextEditor.TextArea;
            var text = textArea.Text;

            var indexes = MatchCase ? text.AllIndexesOf(TextToFind, StringComparison.Ordinal) : text.AllIndexesOf(TextToFind);
            //var indexes = MatchCase ? Regex.Matches(text, TextToFind).Select(m => m.Index).ToList()
            //                        : Regex.Matches(text, TextToFind, RegexOptions.IgnoreCase).Select(m => m.Index).ToList();

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
                var message = $"{mainViewModel.LocaleManager.LoadString("NoFindResults")} '{TextToFind}'.";
                await mainViewModel.DialogService.ShowInformationAsync(message);
            }
        }
    }

    public enum SearchDirection
    {
        Down,
        Up
    }
}
