using ModernNotepadLibrary.Services;
using ModernWpf.Controls;
using System.Threading.Tasks;
using System.Windows;

namespace ModernNotepad.Dialogs
{
    class ContentDialogService : IContentDialogService
    {
        public async Task<bool?> AskConfirmationAsync(string question)
        {
            var dialog = new ContentDialog
            {
                Content = question,
                PrimaryButtonText = (string)Application.Current.TryFindResource("SaveButton"),
                SecondaryButtonText = (string)Application.Current.TryFindResource("NoSaveButton"),
                CloseButtonText = (string)Application.Current.TryFindResource("CancelButton"),
                Title = Application.Current.TryFindResource("Caption"),
            };
            var result = await dialog.ShowAsync();

            return result switch
            {
                ContentDialogResult.None => null,
                ContentDialogResult.Primary => true,
                ContentDialogResult.Secondary => false,
                _ => null,
            };
        }

        public async Task ShowInformationAsync(string message)
        {
            var dialog = new ContentDialog
            {
                Content = message,
                CloseButtonText = (string)Application.Current.TryFindResource("OkButton"),
                Title = Application.Current.TryFindResource("Caption"),
                Owner = Application.Current.MainWindow,
            };
            await dialog.ShowAsync();
        }
    }
}
