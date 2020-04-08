using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace ModernNotepad.Behaviors
{
    class SelectionChangedBehavior : Behavior<TextBox>
    {
        public static readonly DependencyProperty CurrentLineProperty =
            DependencyProperty.Register("CurrentLine", typeof(string), typeof(SelectionChangedBehavior), new PropertyMetadata(null));

        public string CurrentLine
        {
            get => (string)GetValue(CurrentLineProperty);
            set => SetValue(CurrentLineProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();            
            AssociatedObject.SelectionChanged += OnSelectionChanged;
        }        

        protected override void OnDetaching()
        {
            base.OnDetaching();            
            AssociatedObject.SelectionChanged -= OnSelectionChanged;
        }

        private void OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            var charIndex = AssociatedObject.SelectionStart;
            var currentLine = AssociatedObject.GetLineIndexFromCharacterIndex(charIndex) + 1;
            CurrentLine = $"{Application.Current.TryFindResource("CurrentLine")}: {currentLine}";
        }
    }
}
