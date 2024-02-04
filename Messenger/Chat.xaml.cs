using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Messenger
{
    /// <summary>
    /// Логика взаимодействия для Chat.xaml
    /// </summary>
    public partial class Chat : Window
    {
        // Объект для выполнения SQL-команд
        SqlCommand sqlCommand;

        // Объект пользователя, представляющий текущего пользователя чата
        User user;

        // Конструктор окна чата, принимающий пользователя в качестве параметра
        public Chat(User user)
        {
            InitializeComponent();

            // Инициализация пользователя
            this.user = user;

            // Установка имени пользователя в соответствующий элемент управления
            userName.Text = user.nickname;

            // Обновление чата при открытии окна
            UpdateChat();
        }

        // Метод для обновления содержимого чата
        private void UpdateChat()
        {
            // Очистка области сообщений перед обновлением
            messagesArea.Children.Clear();

            // SQL-запрос для получения сообщений с именем отправителя
            string query = "SELECT USERS.NICKNAME, ID_SENDER, TEXT FROM MESSAGES JOIN USERS ON USERS.ID = MESSAGES.ID_RECIPIENT ORDER BY DATA;";

            // Инициализация SQL-команды с использованием запроса и соединения из главного окна
            sqlCommand = new SqlCommand(query, MainWindow.conn);

            // Выполнение запроса и получение результатов в SqlDataReader
            SqlDataReader reader = sqlCommand.ExecuteReader();

            // Чтение результатов запроса и добавление текстовых блоков в область сообщений
            while (reader.Read())
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = reader[0] + "\t";

                // Выравнивание текстового блока вправо, если отправитель не текущий пользователь
                if ((int)reader[1] != user.id)
                    textBlock.HorizontalAlignment = HorizontalAlignment.Right;

                textBlock.FontSize = 20;
                messagesArea.Children.Add(textBlock);

                textBlock = new TextBlock();
                textBlock.Text = reader[2].ToString();

                // Выравнивание текстового блока вправо, если отправитель не текущий пользователь
                if ((int)reader[1] != user.id)
                textBlock.HorizontalAlignment = HorizontalAlignment.Right;

                textBlock.FontSize = 25;
                messagesArea.Children.Add(textBlock);
            }

            // Прокрутка вниз для отображения последнего сообщения
            scroll.ScrollToEnd();

            // Закрытие SqlDataReader после использования
            reader.Close();
        }

        // Обработчик кнопки обновления чата
        private void Button_Update(object sender, RoutedEventArgs e)
        {
            // Вызов метода обновления чата при нажатии кнопки
            UpdateChat();
        }

        // Обработчик кнопки отправки сообщения
        private void Button_Send(object sender, RoutedEventArgs e)
        {
            // SQL-запрос для вставки нового сообщения в базу данных
            string query = $"INSERT INTO MESSAGES VALUES ('{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',{(user.id == 1 ? 2 : 1)}, {user.id}, '{messageBox.Text}')";

            // Инициализация SQL-команды с использованием запроса и соединения из главного окна
            sqlCommand = new SqlCommand(query, MainWindow.conn);

            // Выполнение SQL-команды для вставки нового сообщения
            sqlCommand.ExecuteNonQuery();

            // Очистка текстового поля после отправки сообщения
            messageBox.Text = "";

            // Обновление чата после отправки сообщения
            UpdateChat();
        }

        // Обработчик кнопки выхода из окна чата
        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            // Создание нового главного окна
            MainWindow main = new MainWindow();

            // Отображение нового главного окна
            main.Show();

            // Закрытие текущего окна чата
            this.Close();
        }
    }
}
