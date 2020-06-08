using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ModernNotepad.Behaviors
{
    class ZoomBehavior : Behavior<UIElement>
    {
        public double ZoomScale
        {
            get => (double)GetValue(ZoomScaleProperty);
            set => SetValue(ZoomScaleProperty, value);
        }

        public static readonly DependencyProperty ZoomScaleProperty =
            DependencyProperty.Register("ZoomScale", typeof(double), typeof(ZoomBehavior), new PropertyMetadata(1.0, OnScaleChanged));

        private static void OnScaleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as ZoomBehavior;
            var st = (ScaleTransform)behavior.AssociatedObject.RenderTransform;
            st.ScaleX = (double)e.NewValue;
            st.ScaleY = (double)e.NewValue;
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewMouseWheel += OnPreviewMouseWheel;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewMouseWheel -= OnPreviewMouseWheel;
        }

        private void OnPreviewMouseWheel(object _, MouseWheelEventArgs e)
        {
            SetZoom(e.Delta);
            e.Handled = true;
        }

        private void SetZoom(int delta)
        {
            EnsureRenderTransform(AssociatedObject);
            var st = (ScaleTransform)AssociatedObject.RenderTransform;

            if (delta > 0)
            {
                ZoomScale += 0.1;
            }
            else
            {
                ZoomScale -= 0.1;
            }
            st.ScaleX = ZoomScale;
            st.ScaleY = ZoomScale;
        }

        private void EnsureRenderTransform(UIElement element)
        {
            if (element.RenderTransform == null || !(element.RenderTransform is ScaleTransform))
            {
                element.RenderTransform = new ScaleTransform(1.0, 1.0);
            }
        }
    }
}

