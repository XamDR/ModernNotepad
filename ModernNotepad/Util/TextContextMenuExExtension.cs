using ModernNotepad.CustomControls;
using System;
using System.Threading;
using System.Windows.Markup;

namespace ModernNotepad.Util
{
    class TextContextMenuExExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return DefaultContextMenu.Value;
        }

        private static readonly ThreadLocal<TextContextMenuEx> DefaultContextMenu 
            = new ThreadLocal<TextContextMenuEx>(() => new TextContextMenuEx());
    }
}
