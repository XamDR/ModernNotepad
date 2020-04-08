using ModernNotepadLibrary.Helpers;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Input;

namespace ModernNotepadLibrary.ViewModels
{
    public class AboutWindowViewModel : BaseViewModel
    {
        private readonly MainWindowViewModel mainViewModel;

        public AboutWindowViewModel(MainWindowViewModel mainViewModel) => this.mainViewModel = mainViewModel;

        public string AssemblyCompany
        {
            get
            {
                var attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                return attributes.Length == 0 ? string.Empty : ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                var attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                return attributes.Length == 0 ? string.Empty : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }
        
        public string AssemblyDescription => mainViewModel.LocaleManager.LoadString("AppDescription");

        public string AssemblyProduct
        {
            get
            {
                var attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                return attributes.Length == 0 ? string.Empty : ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyTitle
        {
            get
            {
                var attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);

                if (attributes.Length > 0)
                {
                    var titleAttribute = (AssemblyTitleAttribute)attributes[0];

                    if (titleAttribute.Title != string.Empty)
                    {
                        return titleAttribute.Title;
                    }
                }
                return Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().CodeBase);
            }
        }

        public string AssemblyVersion => Assembly.GetEntryAssembly().GetName().Version.ToString();

        public ICommand CloseWindowCommand => new DelegateCommand(CloseWindow);

        public ICommand OpenHyperlinkCommand => new DelegateCommand<Uri>(OpenHyperlink);

        private void CloseWindow() => mainViewModel.WindowService.Close(GetType());

        private void OpenHyperlink(Uri uri)
        {
            var psi = new ProcessStartInfo
            {
                FileName = uri.OriginalString,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
