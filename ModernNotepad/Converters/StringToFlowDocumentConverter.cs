using ModernNotepad.Services;
using ModernNotepadLibrary.ViewModels;
using System;
using System.Globalization;
using System.Windows;

namespace ModernNotepad.Converters
{
    class StringToFlowDocumentConverter : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var mainWindow = Application.Current.MainWindow;

            if (value != null && mainWindow.DataContext is MainViewModel viewModel)
            {
                var content = value as string;
                var printService = viewModel.PrintService as PrintService;
                return printService.CreateDocument(content);
            }
            return null;
        }
    }
}
