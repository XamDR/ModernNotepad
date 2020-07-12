using Microsoft.Xaml.Behaviors;
using ModernNotepadLibrary.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ModernNotepad.Behaviors
{
    class PasteContentBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            DataObject.AddPastingHandler(AssociatedObject, OnPaste);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            DataObject.RemovePastingHandler(AssociatedObject, OnPaste);
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text, true) ||
                e.DataObject.GetDataPresent(DataFormats.UnicodeText, true))
            {
                var viewModel = Application.Current.MainWindow.DataContext as MainViewModel;
                viewModel.Title = $"*{viewModel.Title.Replace("*", "")}";
                viewModel.TextEditor.UnsavedChanges = true;
                //AssociatedObject.InvalidateVisual();
            }
        }
    }
}
