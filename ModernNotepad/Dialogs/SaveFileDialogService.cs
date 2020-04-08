using Microsoft.Win32;
using ModernNotepadLibrary.Services;

namespace ModernNotepad.Dialogs
{
    class SaveFileDialogService : ISaveFileService
    {
        private readonly SaveFileDialog sfd;

        public SaveFileDialogService() => sfd = new SaveFileDialog();

        public bool AddExtension
        {
            get => sfd.AddExtension;
            set => sfd.AddExtension = value;
        }

        public string DefaultExtension
        {
            get => sfd.DefaultExt;
            set => sfd.DefaultExt = value;
        }

        public string FileName
        {
            get => sfd.FileName;
            set => sfd.FileName = value;
        }        

        public string Filter
        {
            get => sfd.Filter;
            set => sfd.Filter = value;
        }

        public int FilterIndex
        {
            get => sfd.FilterIndex;
            set => sfd.FilterIndex = value;
        }

        public string Title
        {
            get => sfd.Title;
            set => sfd.Title = value;
        }

        public bool? ShowDialog() => sfd.ShowDialog();
    }
}
