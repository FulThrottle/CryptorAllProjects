using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CryptorLogin.Views;

namespace CryptorLogin
{
    public partial class MainWindow : Window
    {
        //private WindowsHelloAuth windowsHelloAuth = new WindowsHelloAuth();

        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = new HomeView(); // Відкриваємо HomeView за замовчуванням

            //this.Hide(); // Ховаємо головне вікно
            //PerformAuthenticationWithLoading(); // Викликаємо функцію з індикатором завантаження
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Close();

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new HomeView();
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new EncryptView();
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new DecryptView();
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new OptionsView();
        }

        private void Applications_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ApplicationsView();
        }

        private void EncryptViewer_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new DocumentsView();
        }
        
        private void SettingsViewer_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new SettingsView();
        }
        
        private void Instruction_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new InstructionView();
        }

        //private async void PerformAuthenticationWithLoading()
        //{
        //    // Відкриваємо завантажувальне вікно
        //    var loadingView = new LoadingView();
        //    loadingView.Show();

        //    // Викликаємо авторизацію через DLL
        //    bool isAuthenticated = await windowsHelloAuth.AuthenticateAsync();

        //    // Закриваємо завантажувальне вікно
        //    loadingView.Close();

        //    if (isAuthenticated)
        //    {
        //        // Якщо авторизація успішна, відкриваємо основну вьюшку
        //        OpenMainView();
        //    }
        //    else
        //    {
        //        // Якщо авторизація не вдалася, показуємо повідомлення і закриваємо програму
        //        MessageBox.Show("Authentication failed. Application will close.");
        //        Application.Current.Shutdown();
        //    }
        //}

        //private void OpenMainView()
        //{
        //    // Відкриваємо другу вьюшку
        //    var mainView = new MainView();
        //    mainView.Show();
        //    this.Close(); // Закриваємо головне вікно
        //}
    }
}
