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
    /// Логика взаимодействия для ReleseManager.xaml
    /// </summary>
    public partial class ReleseManager : Window
    {
        /// <summary>
        /// Номер выбранного заказа
        /// </summary>
        string SelectItem;

        DataBaseEntities db = new DataBaseEntities();

        public ReleseManager()
        {
            InitializeComponent();
            _tablo1.ItemsSource = db.Заказы.Where(x => x.Тип_строки == "Заказ").ToList();
        }
        private void Zakaz_Click(object sender, RoutedEventArgs e)
        {
            SelectItem = (sender as Button).Content.ToString();
            _tablo2.ItemsSource = db.Заказы.Where(x => x.Тип_строки == "Товар" && x.Номер == SelectItem).ToList();
            ButtonsEnabledAndStausUpdate();
        }

        private void ChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            var SelectString = db.Заказы.First(x => x.Номер == SelectItem && x.Тип_строки == "Заказ");

            string SelectButton = (sender as Button).Content.ToString();
            if(SelectButton == "Готов")
            {
                SelectString.Статус = "Готов";
            }
            else if (SelectButton == "Готовится")
            {
                SelectString.Статус = "Готовится";
            }
            else if (SelectButton == "Выдан")
            {
                SelectString.Статус = "Выдан";
            }
            db.SaveChanges();
            ButtonsEnabledAndStausUpdate();
            _tablo1.ItemsSource = db.Заказы.Where(x => x.Тип_строки == "Заказ").ToList();
            MessageBox.Show("Статус изменён", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void ButtonsEnabledAndStausUpdate()
        {
            var SelectString = db.Заказы.First(x => x.Номер == SelectItem && x.Тип_строки == "Заказ");
            string StatusNow = SelectString.Статус;
            if (StatusNow == "Готов")
            {
                Gotov.IsEnabled = false;
                Gotovitsya.IsEnabled = true;
                Vidan.IsEnabled = true;
            }
            else if (StatusNow == "Готовится")
            {
                Gotov.IsEnabled = true;
                Gotovitsya.IsEnabled = false;
                Vidan.IsEnabled = true;
            }
            else if (StatusNow == "Выдан")
            {
                Gotov.IsEnabled = true;
                Gotovitsya.IsEnabled = true;
                Vidan.IsEnabled = false;
            }
            OrderStatus.Text = "Выбран заказ: " + SelectItem;
        }
    }
}
