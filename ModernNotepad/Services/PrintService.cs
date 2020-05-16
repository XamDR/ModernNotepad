using ModernNotepad.Util;
using ModernNotepad.Views;
using ModernNotepadLibrary.Services;
using System.Windows;

namespace ModernNotepad.Services
{
    class PrintService : IPrintService
    {
        public void PrintDocument()
        {            
            if (PrintDialogWrapper.IsPrintJobSendToQueue)
            {
                var document = (Application.Current.MainWindow as MainWindow).Document;
                var documentPaginator = document.DocumentPaginator;
                var description = (string)Application.Current.TryFindResource("DescriptionPrintJob");
                PrintDialogWrapper.PrintDocument(documentPaginator, description);
            }
        }
    }
}
