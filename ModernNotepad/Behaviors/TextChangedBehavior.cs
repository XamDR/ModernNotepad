using Microsoft.Xaml.Behaviors;
using ModernNotepadLibrary.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace ModernNotepad.Behaviors
{
    class TextChangedBehavior : Behavior<TextBox>
    {
        public static readonly DependencyProperty LineCountProperty =
            DependencyProperty.Register("LineCount", typeof(string), typeof(TextChangedBehavior), new PropertyMetadata(null));

        public string LineCount
        {
            get => (string)GetValue(LineCountProperty);
            set => SetValue(LineCountProperty, value);
        }

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
            LineCount = $"{Application.Current.TryFindResource("LineCount")}: {AssociatedObject.LineCount}";
            
            var viewModel = Application.Current.MainWindow.DataContext as MainWindowViewModel;            
            var textArea = viewModel.TextEditor.TextArea;

            if (e.Changes.FirstOrDefault().AddedLength != textArea.Text.Length || 
                e.Changes.FirstOrDefault().RemovedLength > 0)
            {                
                viewModel.Title = $"*{viewModel.Title.Replace("*", "")}";
                viewModel.TextEditor.UnsavedChanges = true;
            }
        }
    }
}
