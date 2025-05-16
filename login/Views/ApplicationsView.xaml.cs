using System.Windows;
using System.Windows.Controls;
using CryptorLogin.Models;

namespace CryptorLogin.Views
{
    public partial class ApplicationsView : UserControl
    {
        public ApplicationsView()
        {
            InitializeComponent();
            LoadHistoryData();
        }

        private void LoadHistoryData()
        {
            HistoryDataGrid.ItemsSource = HistoryManager.LoadHistory();
        }

        private void ClearHistory_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Ви впевнені, що хочете очистити історію переглядів?",
                                         "Підтвердження",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                HistoryManager.ClearHistory();
                LoadHistoryData(); // оновлюємо дані після очищення
            }
        }
    }
}
