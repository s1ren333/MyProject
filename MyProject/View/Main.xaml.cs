using MyProject.ViewModels;
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
using System.Windows.Shapes;
using MyProject.Models;

namespace MyProject.View
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private string connectionString = "Data Source=localhost;Initial Catalog=User;Integrated Security=True";
        public Main()
        {
            InitializeComponent();
            DataContext = new UserViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Registor f = new Registor();
            f.Show();
        }
      
        private void Window_Loaded(object sender, RoutedEventArgs e)
        { 

            SqlConnection connection = new SqlConnection("Data Source = localhost; Initial Catalog = User; Integrated Security = True");

            connection.Open();
            string cmd = "SELECT * FROM user_table1"; // Из какой таблицы нужен вывод 
            SqlCommand createCommand = new SqlCommand(cmd, connection);
            createCommand.ExecuteNonQuery();

            SqlDataAdapter dataAdp = new SqlDataAdapter(createCommand);
            DataTable dt = new DataTable("user_table1"); // В скобках указываем название таблицы
            dataAdp.Fill(dt);
            dataGrid.ItemsSource = dt.DefaultView; // Сам вывод 
            connection.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection("Data Source = localhost; Initial Catalog = User; Integrated Security = True");

            connection.Open();
            string cmd = "SELECT * FROM user_table1"; // Из какой таблицы нужен вывод 
            SqlCommand createCommand = new SqlCommand(cmd, connection);
            createCommand.ExecuteNonQuery();

            SqlDataAdapter dataAdp = new SqlDataAdapter(createCommand);
            DataTable dt = new DataTable("user_table1"); // В скобках указываем название таблицы
            dataAdp.Fill(dt);
            dataGrid.ItemsSource = dt.DefaultView; // Сам вывод 
            connection.Close();
        }
    }
    }

