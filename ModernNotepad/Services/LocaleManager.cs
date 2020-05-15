using ModernNotepad.Locales;
using ModernNotepadLibrary.Services;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace ModernNotepad.Services
{
    class LocaleManager : ILocaleManager
    {
        private readonly LocaleRepository repository = new LocaleRepository();

        public bool IsLocalAvailable 
            => Application.Current.Resources.MergedDictionaries
            .Where(r => r.Source != null)
            .SingleOrDefault(r => r.Source.OriginalString.Replace(".xaml", "").EndsWith(CultureInfo.CurrentUICulture.Name)) != null;

        public string LoadString(string key)
        {
            if (IsLocalAvailable)
            {
                var dictionary = repository.Dictionaries.SingleOrDefault(d => d.Key == CultureInfo.CurrentUICulture.Name);
                return dictionary.Value[key];
            }
            else
            {
                return repository.EnglishDictionary[key];
            }
        }

        public void LoadStringResource(string locale)
        {            
            var resource = new ResourceDictionary
            {
                Source = new Uri($"pack://application:,,,/Locales/Strings_{locale}.xaml")
            };
            Application.Current.Resources.MergedDictionaries.Add(resource);
        }
    }
}
