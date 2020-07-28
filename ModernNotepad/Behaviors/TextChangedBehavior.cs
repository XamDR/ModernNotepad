using Microsoft.Xaml.Behaviors;
using ModernNotepadLibrary.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace ModernNotepad.Behaviors
{
    class TextChangedBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();            
            AssociatedObject.TextChanged += OnTextChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.TextChanged -= OnTextChanged;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {   
            var viewModel = Application.Current.MainWindow.DataContext as MainViewModel;
            var editor = viewModel.TextEditor;
            var textArea = editor.TextArea;

            if ((!editor.SavedAsFile && e.Changes.FirstOrDefault().AddedLength > 0) ||
                e.Changes.FirstOrDefault().AddedLength != textArea.Text.Length ||
                e.Changes.FirstOrDefault().RemovedLength > 0)
            {
                viewModel.Title = $"*{viewModel.Title.Replace("*", "")}";
                viewModel.TextEditor.UnsavedChanges = true;
            }
        }
    }
}
