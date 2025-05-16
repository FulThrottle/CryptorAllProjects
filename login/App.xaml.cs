using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using CryptorWinUIDLL;
using Syncfusion.Licensing;


namespace CryptorLogin
{
    public partial class App : Application
    {
        private WindowsHelloAuth windowsHelloAuth = new WindowsHelloAuth();

        public App()
        {
            SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF1cXGJCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWXxfeHVdRWlcVUd/W0s=");
        }

        // Звичайний метод для делегата StartupEventHandler
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            _ = RunAsync(); // запускаємо асинхронний запуск у фоні
        }

        private async Task RunAsync()
        {
            bool isAuthenticated = await AuthenticateUserAsync();

            if (isAuthenticated)
            {
                var mainWindow = new MainWindow();
                MainWindow = mainWindow;
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Authentication failed. Application will close.");
                Shutdown();
            }
        }

        private async Task<bool> AuthenticateUserAsync()
        {
            try
            {
                bool isAvailable = await windowsHelloAuth.IsWindowsHelloAvailableAsync();
                if (!isAvailable)
                {
                    MessageBox.Show("Windows Hello is not available.");
                    return false;
                }

                await Task.Delay(100);

                return await windowsHelloAuth.AuthenticateAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return false;
            }
        }
    }

}
