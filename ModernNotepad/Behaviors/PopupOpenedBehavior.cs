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
            var target = (Window)AssociatedObject.PlacementTarget;
            AssociatedObject.HorizontalOffset = (target.Width - child.ActualWidth) / 2;            
            AssociatedObject.VerticalOffset = (Application.Current.MainWindow.Height - target.Height) / 2 - 100;

            await Task.Delay(2000);
            AssociatedObject.IsOpen = false;
        }
    }
}
