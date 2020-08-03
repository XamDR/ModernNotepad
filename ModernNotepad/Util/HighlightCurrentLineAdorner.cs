using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;

namespace ModernNotepad.Util
{
    public class HighlightCurrentLineAdorner : Adorner
    {
        private readonly DispatcherTimer timer;
        private bool shouldChangeVisibility = false;

        public HighlightCurrentLineAdorner(UIElement adornedElement) : base(adornedElement)
        {
            IsHitTestVisible = false;
            (AdornedElement as TextBox).SelectionChanged += OnAdornedElement_SelectionChanged;
            (AdornedElement as TextBox).AddHandler(ScrollViewer.ScrollChangedEvent, new ScrollChangedEventHandler(OnAdornedElement_ScrollChanged));
            timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(50), IsEnabled = false };
            timer.Tick += Timer_Tick;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            var textArea = AdornedElement as TextBox;            
            try
            {
                // We try to draw the highlight rectangle, but this can fail because the 
                // TextBox sometimes doesn't give the correct values, e.g., LineCount can be equal to 0. 
                // So, in the method DrawHighlightRect(), the line: 
                // var rect = textArea.GetRectFromCharacterIndex(textArea.GetCharacterIndexFromLineIndex(currentLine));
                // can throw an exception because currentLine will be equal to -1.
                // Therefore, we need to 'wait' to the TextBox to give us the right values. 
                // Thus, we use a timer to wait 50 ms, and then we call InvalidateVisual()
                // which will call OnRender() again. This time the method DrawHighlightRect() will work.
                
                // We draw the rectangle to highlight the current line when there is no selected text
                if (textArea.SelectionLength == 0)
                {
                    DrawHighlightRect(drawingContext, textArea);
                }
            }
            catch (Exception)
            {
                timer.IsEnabled = true; //this means the TextBox gave us incorrect values.
            }
        }

        private void OnAdornedElement_SelectionChanged(object sender, RoutedEventArgs e)
        {
            InvalidateVisual();
            if (shouldChangeVisibility)
            {
                Visibility = Visibility.Visible;
                shouldChangeVisibility = false;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.IsEnabled = false;
            InvalidateVisual();
        }

        private void DrawHighlightRect(DrawingContext drawingContext, TextBox textBox)
        {   
            var rect = textBox.GetRectFromCharacterIndex(textBox.CaretIndex);
            var highlightRect = new Rect(0, rect.Y, textBox.ActualWidth, rect.Height);
            var brush = Brushes.Transparent;
            var pen = new Pen(Brushes.Gray, 1.0);
            drawingContext.DrawRectangle(brush, pen, highlightRect);
        }

        private void OnAdornedElement_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (IsCaretVisible((TextBox)AdornedElement))
            {
                InvalidateVisual();                
                if (shouldChangeVisibility)
                {
                    Visibility = Visibility.Visible;
                    shouldChangeVisibility = false;
                }
            }
            else
            {   
                Visibility = Visibility.Collapsed;
                shouldChangeVisibility = true;
            }
        }

        private bool IsCaretVisible(TextBox textBox)
        {
            var currentLine = textBox.GetLineIndexFromCharacterIndex(textBox.SelectionStart);
            var firstVisibleLine = textBox.GetFirstVisibleLineIndex();
            var lastVisibleLine = textBox.GetLastVisibleLineIndex();
            return firstVisibleLine <= currentLine && currentLine <= lastVisibleLine;
        }
    }
}
