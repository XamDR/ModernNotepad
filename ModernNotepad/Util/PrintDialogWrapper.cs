using System.Windows.Controls;
using System.Windows.Documents;

namespace ModernNotepad.Util
{
    static class PrintDialogWrapper
    {
        public static PrintDialog PrintDialog { get; } = new PrintDialog();

        public static bool IsPrintJobSendToQueue => PrintDialog.ShowDialog() == true;

        public static double PrintableAreaHeight => PrintDialog.PrintableAreaHeight;

        public static double PrintableAreaWidth => PrintDialog.PrintableAreaWidth;

        public static void PrintDocument(DocumentPaginator documentPaginator, string description) 
            => PrintDialog.PrintDocument(documentPaginator, description);
    }
}
