using System;
using System.Linq;
using System.Windows;

namespace FoodExpress
{
    class GuestInfo
    {
        DataBaseEntities db = new DataBaseEntities();

        /// <summary>
        /// Код заказа, генерирующийся в момент перехода в форму гостя и состоящий из одной случайной буквы и из двух случайных цифр (Представляет собой примерно A01).
        /// </summary>
        public static string GuestCode;

        /// <summary>
        /// Метод для случайной генерации кода заказа. Изначально генерируется буква от 'A' до 'Z' и цифры от 0 до 9.
        /// </summary>
        public void GenerateCode()
        {
            Random rand = new Random();
            char RandomChar = (char)rand.Next('A', 'Z' + 1);
            int RandomFirstNumber = rand.Next(0, 9);
            int RandomSecondNumber = rand.Next(0, 9);

            GuestCode = RandomChar.ToString() + RandomFirstNumber.ToString() + RandomSecondNumber.ToString();
            try
            {
                db.Заказы.First(x => x.Номер == GuestCode);
                GenerateCode();
            }
            catch
            {

            }
        }
    }
}
