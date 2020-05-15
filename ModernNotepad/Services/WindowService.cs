using ModernNotepad.Views;
using ModernNotepadLibrary.Core;
using ModernNotepadLibrary.Services;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace ModernNotepad.Services
{
    class WindowService : IWindowService
    {
        public void Close(Type viewModelType)
        {
            var mainWindow = Application.Current.MainWindow;
            var childWindow = mainWindow.OwnedWindows
                            .Cast<Window>()
                            .SingleOrDefault(w => w.GetType() == GetViewType(viewModelType));
            childWindow.Close();
        }

        public void CloseMainWindow() => Application.Current.MainWindow.Close();

        public void Show(object viewModel, Type viewModelType)
        {
            var window = CreateWindow(viewModelType);
            window.DataContext = viewModel;
            window.Owner = Application.Current.MainWindow;
            window.Show();
        }

        public bool? ShowDialog(object viewModel, Type viewModelType)
        {
            var window = CreateWindow(viewModelType);
            window.DataContext = viewModel;
            window.Owner = Application.Current.MainWindow;            
            return window.ShowDialog();
        }

        public IMainView CreateMainView(object viewModel, Type viewModelType)
        {
            var window = CreateWindow(viewModelType) as MainWindow;
            window.DataContext = viewModel;
            window.Show();
            return window;
        }

        private Type GetViewType(Type viewModelType)
        {
            var viewModelFullName = viewModelType.FullName; //ModernNotepadLibrary.ViewModels.AboutViewModel

            var names = viewModelFullName.Split('.');
            var firstName = names[0].Replace("Library", "");
            var secondName = names[1].Replace("Model", "");
            var thirdName = names[2].Replace("ViewModel", "Window");

            var viewFullName = string.Join('.', new[] { firstName, secondName, thirdName }); //ModernNotepad.Views.AboutWindow
            var viewType = Assembly.GetExecutingAssembly().GetType(viewFullName);

            return viewType;
        }

        private Window CreateWindow(Type viewModelType)
        {
            var viewType = GetViewType(viewModelType);
            return Activator.CreateInstance(viewType) as Window;
        }
    }
}
