using Microsoft.Xaml.Behaviors;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ModernNotepad.Behaviors
{
    class PopupOpenedBehavior : Behavior<Popup>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Opened += OnPopupOpened;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Opened -= OnPopupOpened;
        }

        private async void OnPopupOpened(object sender, EventArgs e)
        {
            var child = (Border)AssociatedObject.Child;            
            AssociatedObject.HorizontalOffset = (Application.Current.MainWindow.ActualWidth - child.ActualWidth) / 2;
            AssociatedObject.VerticalOffset = -100.0;

            await Task.Delay(2000);
            AssociatedObject.IsOpen = false;
        }
    }
}
