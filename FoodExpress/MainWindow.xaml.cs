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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoodExpress
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            Products ProductsForm = new Products();
            ProductsForm.Show();
        }

        private void TabloButton_Click(object sender, RoutedEventArgs e)
        {
            Tablo tablo = new Tablo();
            tablo.Show();
        }

        private void PersonalButton_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
        }
    }
}
