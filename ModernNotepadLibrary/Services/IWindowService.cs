using ModernNotepadLibrary.Core;
using System;

namespace ModernNotepadLibrary.Services
{
    public interface IWindowService
    {
        void Close(Type viewModelType);
        void CloseMainWindow();
        IMainView CreateMainView(object viewModel, Type viewModelType);
        void Show(object viewModel, Type viewModelType);
        bool? ShowDialog(object viewModel, Type viewModelType);        
    }
}
