using ModernNotepad.Views;
using ModernNotepadLibrary.ViewModels;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ModernNotepad.Util
{
    class StringToFlowDocumentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var content = value as string;
                var viewModel = (Application.Current.MainWindow as MainWindow).DataContext as MainWindowViewModel;
                var printService = viewModel.PrintService as PrintService;
                return printService.CreateDocument(content);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
