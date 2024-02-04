using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

namespace Messenger
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Статическое поле для установления соединения с базой данных
        public static SqlConnection conn = null;

        // Конструктор главного окна
        public MainWindow()
        {
            InitializeComponent();

            // Проверка наличия открытого соединения с базой данных
            if (conn == null)
            {
                // Инициализация соединения с локальной базой данных MSSQL
                conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Users; Integrated Security=SSPI;");

                // Открытие соединения
                conn.Open();
            }

            // Включение кнопки входа
            LogInButton.IsEnabled = true;
        }

        // Обработчик кнопки входа
        private void Button_Enter(object sender, RoutedEventArgs e)
        {
            // Создание SQL-команды для выполнения запроса к базе данных
            SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM USERS WHERE LOGIN = '{loginBox.Text}' and PASSWORD = '{passBox.Password}';", conn);

            // Выполнение запроса и получение результата в SqlDataReader
            SqlDataReader reader = sqlCommand.ExecuteReader();

            // Проверка наличия записей в результирующем наборе
            if (reader.Read())
            {
                // Создание объекта пользователя с использованием данных из результата запроса
                User user = new User((int)reader[0], reader[1].ToString());

                // Закрытие SqlDataReader после использования
                reader.Close();

                // Создание и отображение второго окна (чата) с передачей созданного пользователя
                Chat secondWindow = new Chat(user);
                secondWindow.Show();

                // Закрытие текущего окна
                this.Close();
            }
            else
            {
                // Установка подсказок и изменение цвета фона полей в случае неверного логина или пароля
                loginBox.ToolTip = "Неверный логин или пароль";
                loginBox.Background = Brushes.LightSkyBlue;
                passBox.ToolTip = "Неверный логин или пароль";
                passBox.Background = Brushes.LightSkyBlue;
            }
        }
    }
}



