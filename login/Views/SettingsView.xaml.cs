using System.Windows;
using System.Windows.Controls;

namespace CryptorLogin.Views
{
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            LoadCurrentSettings();
        }

        private void LoadCurrentSettings()
        {
            var settings = Models.AppSettings.Load();

            // Завантажуємо поточну тему
            foreach (ComboBoxItem item in ThemeSelector.Items)
            {
                if ((string)item.Tag == settings.SelectedTheme)
                    item.IsSelected = true;
            }

            // Завантажуємо поточну мову
            foreach (ComboBoxItem item in LanguageSelector.Items)
            {
                if ((string)item.Tag == settings.SelectedLanguage)
                    item.IsSelected = true;
            }
        }

        private void ThemeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ThemeSelector.SelectedItem is ComboBoxItem selectedItem)
            {
                string theme = selectedItem.Tag.ToString();
                Models.ThemeManager.ChangeTheme(theme);

                var settings = Models.AppSettings.Load();
                settings.SelectedTheme = theme;
                settings.Save();
            }
        }

        private void LanguageSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LanguageSelector.SelectedItem is ComboBoxItem selectedItem)
            {
                string language = selectedItem.Tag.ToString();
                Models.LocalizationManager.SetLanguage(language);

                var settings = Models.AppSettings.Load();
                settings.SelectedLanguage = language;
                settings.Save();

                //MessageBox.Show("Мова застосунку змінена. Перезапустіть застосунок для повного застосування змін.",
                //                "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
