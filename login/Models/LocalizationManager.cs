using System.Globalization;
using System.Threading;
using System.Windows;

namespace CryptorLogin.Models
{
    public static class LocalizationManager
    {
        public static void SetLanguage(string lang)
        {
            var culture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = culture;

            // Тут може бути додатковий код, якщо потрібне оновлення інтерфейсу в реальному часі
        }
    }
}
