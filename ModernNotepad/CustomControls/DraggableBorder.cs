using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ModernNotepad.CustomControls
{
    /// <summary>
    /// A draggable border control.
    /// </summary>
    public class DraggableBorder : Border
    {
        private UIElement child = null;
        private Point origin;
        private Point start;

        public override UIElement Child
        {
            get => base.Child;
            set
            {
                if (value != null && value != Child)
                {
                    Initialize(value);
                }
                base.Child = value;
            }
        }

        private TranslateTransform GetTranslateTransform(UIElement element) => (TranslateTransform)element.RenderTransform;

        private void Initialize(UIElement element)
        {
            child = element;
            
            if (child != null)
            {
                var tt = new TranslateTransform();
                child.RenderTransform = tt;
                child.RenderTransformOrigin = new Point(0.0, 0.0);
                MouseLeftButtonDown += OnChild_MouseLeftButtonDown;
                MouseLeftButtonUp += OnChild_MouseLeftButtonUp;
                MouseMove += OnChild_MouseMove;
                PreviewMouseRightButtonDown += OnChild_PreviewMouseRightButtonDown;
            }
        }

        private void Reset()
        {
            if (child != null)
            {                
                var tt = GetTranslateTransform(child); // reset pan
                tt.X = 0.0;
                tt.Y = 0.0;
            }
        }

        private void OnChild_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                var tt = GetTranslateTransform(child);
                start = e.GetPosition(this);
                origin = new Point(tt.X, tt.Y);
                Cursor = Cursors.SizeAll;
                child.CaptureMouse();
            }
        }

        private void OnChild_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                child.ReleaseMouseCapture();
                Cursor = Cursors.Arrow;
            }
        }

        private void OnChild_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e) => Reset();

        private void OnChild_MouseMove(object sender, MouseEventArgs e)
        {
            if (child != null)
            {
                if (child.IsMouseCaptured)
                {
                    var tt = GetTranslateTransform(child);
                    Vector v = start - e.GetPosition(this);
                    tt.X = origin.X - v.X;
                    tt.Y = origin.Y - v.Y;
                }
            }
        }
    }
}
