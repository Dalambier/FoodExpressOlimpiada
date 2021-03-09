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

namespace FoodExpress
{
    /// <summary>
    /// Логика взаимодействия для Tablo.xaml
    /// </summary>
    public partial class Tablo : Window
    {
        DataBaseEntities db = new DataBaseEntities();

        /// <summary>
        /// Вставка заказов в таблицу.
        /// </summary>
        public Tablo()
        {
            InitializeComponent();
            _tablo1.ItemsSource = db.Заказы.Where(x => x.Тип_строки == "Заказ").ToList();
        }
    }
}
