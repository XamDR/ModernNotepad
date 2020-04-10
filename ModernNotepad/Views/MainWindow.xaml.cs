using ModernNotepadLibrary.Core;
using System.Windows;

namespace ModernNotepad.Views
{
    public partial class MainWindow : Window, IMainWindow
    {
        public MainWindow() => InitializeComponent();

        public ITextArea TextArea => textArea;
    }
}
