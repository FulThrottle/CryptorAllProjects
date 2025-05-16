using System.IO;
using System.Text.Json;

namespace CryptorLogin.Models
{
    public class AppSettings
    {
        public string SelectedTheme { get; set; } = "Dark";
        public string SelectedLanguage { get; set; } = "uk-UA";

        private static string SettingsPath => "appsettings.json";

        public static AppSettings Load()
        {
            if (!File.Exists(SettingsPath))
                return new AppSettings();

            string json = File.ReadAllText(SettingsPath);
            return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
        }

        public void Save()
        {
            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsPath, json);
        }
    }
}
