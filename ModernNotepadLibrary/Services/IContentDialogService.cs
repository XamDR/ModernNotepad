using System.Threading.Tasks;

namespace ModernNotepadLibrary.Services
{
    /// <summary>
    /// Provides methods to display content dialogs.
    /// </summary>
    public interface IContentDialogService
    {
        /// <summary>
        /// Displays a content dialog that has a message and title bar caption; and ask for user confirmation.
        /// </summary>
        /// <param name="question">The question asked to the user.</param>
        /// <returns></returns>        
        Task<bool?> AskConfirmationAsync(string question);

        /// <summary>
        /// Displays a content dialog that has a message and title bar caption; and displays information to the user.
        /// </summary>
        /// <param name="message">The message to display to the user.</param>
        /// <returns></returns>        
        Task ShowInformationAsync(string message);
    }
}
