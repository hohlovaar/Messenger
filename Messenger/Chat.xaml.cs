using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

namespace Messenger
{
    /// <summary>
    /// Логика взаимодействия для Chat.xaml
    /// </summary>
    public partial class Chat : Window
    {
        SqlCommand sqlCommand;
        //User user;
        public static MainWindow Window;
        public Chat()
        {
            InitializeComponent();
            //this.user = user;
        }

        private void Button_Send(object sender, RoutedEventArgs e)
        {
            string query = $"INSERT INTO MESSAG VALUES ('{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',{(user.id == 1 ? 2:1)}, {user.id}, '{TextMessageBox.Text}')";
            sqlCommand = new SqlCommand(query, MainWindow.conn);
            sqlCommand.ExecuteNonQuery();
            TextMessageBox.Text = "";
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Window = new MainWindow();
            Window.Show();
            this.Close();
        }
    }
}
