using ModernNotepadLibrary.Services;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ModernNotepad.Util
{
    class PrintService : IPrintService
    {
        private readonly PrintDialog printDialog = new PrintDialog();

        public void PrintText(string content)
        {
            if (printDialog.ShowDialog() == true)
            {
                var document = CreateDocument(content);
                var documentPaginator = ((IDocumentPaginatorSource)document).DocumentPaginator;
                printDialog.PrintDocument(documentPaginator, "Demo");
            }
        }

        public FlowDocument CreateDocument(string content)
        {
            var document = new FlowDocument
            {
                ColumnGap = 0,
                ColumnWidth = printDialog.PrintableAreaWidth,
                PageHeight = printDialog.PrintableAreaHeight,
                PageWidth = printDialog.PrintableAreaWidth,
            };
            var paragraph = new Paragraph();
            paragraph.Inlines.Add(content);
            document.Blocks.Add(paragraph);

            return document;
        }
    }
}
