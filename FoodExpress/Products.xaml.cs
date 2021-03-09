 using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FoodExpress
{
    public partial class Products : Window
    {


        DataBaseEntities db = new DataBaseEntities();

        /// <summary>
        /// Генерация кода и показ категорий.
        /// </summary>
        public Products()
        {
            InitializeComponent();

            GuestInfo guestInfo = new GuestInfo();
            guestInfo.GenerateCode();
            _Kategiryes.ItemsSource = db.Категории.ToList();
        }

        /// <summary>
        /// Открывает список товаров по выбранной категории.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenCategor(object sender, RoutedEventArgs e)
        {
            string SelectedItem = (sender as Button).Content.ToString();
            var SelectedString = db.Категории.FirstOrDefault(x => x.Название == SelectedItem);
            _Products.ItemsSource = db.Товары.Where(x => x.Категория == SelectedString.id).ToList();
        }

        /// <summary>
        /// Помещает выбранный товар в корзину.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductChoose(object sender, RoutedEventArgs e)
        {
            string SelectItem = (sender as Button).Content.ToString();
            var SelectedItem = db.Товары.FirstOrDefault(x => x.Наименование == SelectItem);
            if (CorrectCountProduct())
            {
                int CountProductInt = Int32.Parse(CountProduct.Text);

                try
                {
                    db.Заказы.First(x => x.Номер == GuestInfo.GuestCode && x.Наименование_товара == SelectedItem.Наименование);
                    MessageBox.Show("Данный товар уже в корзине", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    CountProduct.Text = string.Empty;
                }
                catch
                {
                    db.Заказы.Add(new Заказы
                    {
                        Номер = GuestInfo.GuestCode,
                        Статус = "В корзине",
                        Наименование_товара = SelectedItem.Наименование,
                        Количество_товара = CountProductInt,
                        Цена_товара = SelectedItem.Цена * CountProductInt,
                        Тип_строки = "Товар"
                    });
                    db.SaveChanges();
                    _Cart.ItemsSource = db.Заказы.Where(x => x.Номер == GuestInfo.GuestCode).ToList();
                    CountProduct.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Проверка на ввод количества товара.
        /// </summary>
        /// <returns></returns>
        private bool CorrectCountProduct()
        {
            try
            {
                int CheckNumber = Int32.Parse(CountProduct.Text);
                if (CheckNumber <= 0)
                {
                    MessageBox.Show("Некорректный ввод, нельзя покупать пустоту", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    CountProduct.Text = string.Empty;
                    return false;
                }
                else if (CheckNumber >= 100)
                {
                    MessageBox.Show("Некорректный ввод, нельзя покупать так много продуктов", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    CountProduct.Text = string.Empty;
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("Некорректный ввод, нужно вводить целые числа", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                CountProduct.Text = string.Empty;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Удаление товара из корзины.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            string SelectItem = (sender as Button).Content.ToString();
            var SelectString = db.Заказы.First(x=> x.Наименование_товара == SelectItem && x.Номер == GuestInfo.GuestCode);
            db.Заказы.Remove(SelectString);
            db.SaveChanges();
            _Cart.ItemsSource = db.Заказы.Where(x => x.Номер == GuestInfo.GuestCode).ToList();
        }

        private void ConfirmOrderClick(object sender, RoutedEventArgs e)
        {
            try
            {
                db.Заказы.First(x => x.Номер == GuestInfo.GuestCode);
                ConfirmOrder confirmOrder = new ConfirmOrder();
                confirmOrder.ShowDialog();
                if (ConfirmOrder.IsOrdered)
                    this.Close();
            }
            catch
            {
                MessageBox.Show("Заполните корзину товаром, чтобы произвести заказ", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
