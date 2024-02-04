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
        public static Chat ChatWindow;
        public static SqlConnection conn = null;
        public MainWindow()
        {
            if (conn == null)
            {
                conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB; 
                Initial Catalog = Users; Integrated Security=True;");
                conn.Open();
            }
            loginBox.IsEnabled = true;
        }

        private void Button_Enter(object sender, RoutedEventArgs e)
        {
            string login = loginBox.Text;
            string password = passBox.Password;


            string query = "SELECT COUNT(*) FROM Users WHERE Login = @login AND Password = @password";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@login", SqlDbType.NVarChar, 10).Value = login;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar, 10).Value = password;

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                conn.Close();

                if (count > 0)
                {
                    ChatWindow = new Chat();
                    ChatWindow.Show();
                    this.Close();
                }
                else
                {
                    loginBox.ToolTip = "Неверный логин или пароль";
                    loginBox.Background = Brushes.LightSkyBlue;
                    passBox.ToolTip = "Неверный логин или пароль";
                    passBox.Background = Brushes.LightSkyBlue;
                }
            }
        }
    }
}

