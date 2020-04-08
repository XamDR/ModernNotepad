using ModernNotepadLibrary.Core;
using ModernNotepadLibrary.Services;
using System;
using System.IO;
using System.Text.Json;

namespace ModernNotepad.Util
{
    class SettingsManager : ISettingsManager<UserSettings>
    {
        public string SettingsDirectoryPath => GetFullPath().Replace(@"\userdata.json", string.Empty);

        public UserSettings LoadSettings()
        {
            var fullPath = GetFullPath();
            var settings = JsonSerializer.Deserialize<UserSettings>(File.ReadAllText(fullPath));
            return settings;
        }

        public void SaveSettings(UserSettings settings)
        {
            var fullPath = GetFullPath();
            Directory.CreateDirectory(SettingsDirectoryPath);
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(fullPath, JsonSerializer.Serialize(settings, options));
        }

        private string GetFullPath()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(appDataPath, @"ModernNotepad\userdata.json");
        }
    }
}
