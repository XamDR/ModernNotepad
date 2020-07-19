using ModernNotepad.Util;
using System;
using System.Windows;
using System.Windows.Interop;

namespace ModernNotepad.CustomControls
{
    /// <summary>
    /// Child window with a custom system menu.
    /// </summary>
    public class DialogWindow : Window
    {
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var hwnd = new WindowInteropHelper(this).Handle;
            var hSystemMenu = NativeMethods.GetSystemMenu(hwnd, false);
            NativeMethods.DeleteMenu(hSystemMenu, 0, NativeMethods.MF_BYPOSITION);
            NativeMethods.DeleteMenu(hSystemMenu, 1, NativeMethods.MF_BYPOSITION);
            NativeMethods.DeleteMenu(hSystemMenu, 1, NativeMethods.MF_BYPOSITION);
            NativeMethods.DeleteMenu(hSystemMenu, 1, NativeMethods.MF_BYPOSITION);
            NativeMethods.DeleteMenu(hSystemMenu, 1, NativeMethods.MF_BYPOSITION);
        }
    }
}
