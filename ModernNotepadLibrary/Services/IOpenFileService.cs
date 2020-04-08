namespace ModernNotepadLibrary.Services
{
    /// <summary>
    /// Provides properties and methods to be used by dialog windows.
    /// </summary>
    public interface IOpenFileService
    {
        /// <summary>
        /// Gets or sets a value that specifies the default extension string to use to filter the list of files that are displayed.
        /// </summary>
        string DefaultExtension { get; set; }

        /// <summary>
        /// Gets or sets a string containing the full path of the file selected in a file dialog.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Gets an array that contains one file name for each selected file.
        /// </summary>
        string[] FileNames { get; }

        /// <summary>
        /// Gets or sets the filter string that determines what types of files are displayed from this dialog.
        /// </summary>
        string Filter { get; set; }

        /// <summary>
        /// Gets or sets the index of the filter currently selected in a file dialog.
        /// </summary>
        int FilterIndex { get; set; }

        /// <summary>
        /// Gets or sets an option indicating whether OpenFileDialog allows users to select multiple files.
        /// </summary>
        bool MultiSelect { get; set; }

        /// <summary>
        /// Gets or sets the text that appears in the title bar of a file dialog.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Displays a common dialog.
        /// </summary>
        /// <returns>If the user clicks the OK button of the dialog that is displayed true is returned; otherwise, false.</returns>
        bool? ShowDialog();
    }
}
