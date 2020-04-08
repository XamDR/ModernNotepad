using ModernNotepadLibrary.Core;
using System;

namespace ModernNotepadLibrary.Services
{
    public interface IWindowService
    {
        void Close(Type viewModelType);
        void CloseMainWindow();
        void Show(object viewModel, Type viewModelType);
        bool? ShowDialog(object viewModel, Type viewModelType);
        IMainWindow ShowMainWindow(object viewModel, Type viewModelType);
    }
}
