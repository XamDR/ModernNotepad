using Microsoft.Xaml.Behaviors;
using ModernNotepadLibrary.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ModernNotepad.Behaviors
{
    class PasteContentBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewKeyDown += OnPreviewKeyDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewKeyDown -= OnPreviewKeyDown;
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            var viewModel = Application.Current.MainWindow.DataContext as MainViewModel;            
            
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.V)
            {
                viewModel.Title = $"*{viewModel.Title.Replace("*", "")}";
                viewModel.TextEditor.UnsavedChanges = true;
            }
        }
    }
}
