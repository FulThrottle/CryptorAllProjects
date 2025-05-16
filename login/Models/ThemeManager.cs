using System;
using System.Windows;

namespace CryptorLogin.Models
{
    public static class ThemeManager
    {
        public static void ChangeTheme(string theme)
        {
            Application.Current.Resources.MergedDictionaries.Clear();

            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("Themes/CommonStyles.xaml", UriKind.Relative)
            });

            switch (theme)
            {
                case "Dark":
                    Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
                    {
                        Source = new Uri("Themes/DarkTheme.xaml", UriKind.Relative)
                    });
                    break;

                case "Light":
                    Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
                    {
                        Source = new Uri("Themes/LightTheme.xaml", UriKind.Relative)
                    });
                    break;
            }
        }
    }

}
