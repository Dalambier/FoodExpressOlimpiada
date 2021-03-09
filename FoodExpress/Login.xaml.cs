using System.Linq;
using System.Windows;

namespace FoodExpress
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        DataBaseEntities db = new DataBaseEntities();
        public Login()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var SelectUser = db.Пользователи.First(x => x.Логин == LoginField.Text && x.Пароль == PasswordField.Password);
                string SelectRole = SelectUser.Роль;
                if (SelectRole == "Менеджер выдачи")
                {
                    ReleseManager releseManager = new ReleseManager();
                    releseManager.Show();
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
