using ModernNotepadLibrary.Core;
using System.Windows;
using System.Windows.Documents;

namespace ModernNotepad.Views
{
    public partial class MainWindow : Window, IMainView
    {
        public MainWindow() => InitializeComponent();

        public ITextArea TextArea => textArea;

        public IDocumentPaginatorSource Document => pageViewer.Document;
    }
}
