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
    public partial class ConfirmOrder : Window
    {
        DataBaseEntities db = new DataBaseEntities();

        /// <summary>
        /// Проверка - был ли принят заказ.
        /// </summary>
        public static bool IsOrdered = false;


        /// <summary>
        /// Заполнение строк кода и цены.
        /// </summary>
        public ConfirmOrder()
        {
            InitializeComponent();

            IsOrdered = false;
            string Price = CalculatePrice().ToString();
            OrderCode.Text = "Ваш код: " + GuestInfo.GuestCode;
            OrderPrice.Text = "Цена заказа составляет: " + Price;
        }

        /// <summary>
        /// Просчитывание стоимости товара
        /// </summary>
        /// <returns></returns>
        private int CalculatePrice()
        {
            int Price = 0;
            var PriceCount = db.Заказы.Where(x => x.Номер == GuestInfo.GuestCode);
            foreach (var item in PriceCount)
            {
                Price += item.Цена_товара;
            }
            return Price;
        }

        /// <summary>
        /// Кнопка подтверждения заказа.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Confirm(object sender, RoutedEventArgs e)
        {
            db.Заказы.Add(new Заказы
            {
                Номер = GuestInfo.GuestCode,
                Дата = DateTime.Now.ToString(),
                Статус = "Готовится",
                Цена_товара = CalculatePrice(),
                Тип_строки = "Заказ"
            });
            db.SaveChanges();
            MessageBox.Show("Заказ успешно оформлен, не забывайте, ваш код - " + GuestInfo.GuestCode, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            IsOrdered = true;
            this.Close();
        }
    }
}
