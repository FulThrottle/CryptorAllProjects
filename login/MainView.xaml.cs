using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CryptorLogin
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            // Додаємо обробник для події закриття вікна
            this.Closed += MainView_Closed;
        }

        private void MainView_Closed(object sender, EventArgs e)
        {
            // Закриваємо весь застосунок
            Application.Current.Shutdown();
        }
    }

}
