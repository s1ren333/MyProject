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
            ADDuser f = new ADDuser();
            f.Show();
            Close();
        }
      
        private void Window_Loaded(object sender, RoutedEventArgs e)
        { 
            //Для обновления данных в DataGrid

            SqlConnection connection = new SqlConnection("Data Source = localhost; Initial Catalog = User; Integrated Security = True");
            connection.Open();
            string cmd = "SELECT * FROM UserTable"; // Из какой таблицы нужен вывод 
            SqlCommand createCommand = new SqlCommand(cmd, connection);
            createCommand.ExecuteNonQuery();
            SqlDataAdapter dataAdp = new SqlDataAdapter(createCommand);
            DataTable dt = new DataTable("UserTable");
            dataAdp.Fill(dt);
            dataGrid.ItemsSource = dt.DefaultView;
            connection.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection("Data Source = localhost; Initial Catalog = User; Integrated Security = True");
            connection.Open();
            string cmd = "SELECT * FROM UserTable"; // Из какой таблицы нужен вывод 
            SqlCommand createCommand = new SqlCommand(cmd, connection);
            createCommand.ExecuteNonQuery();

            SqlDataAdapter dataAdp = new SqlDataAdapter(createCommand);
            DataTable dt = new DataTable("UserTable"); // В скобках указываем название таблицы
            dataAdp.Fill(dt);
            dataGrid.ItemsSource = dt.DefaultView; // Сам вывод 
            connection.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            setting set = new setting();
            set.Show();
        }


        //Удаление пользователя
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
       
                DataRowView selectedRow = (DataRowView)dataGrid.SelectedItem;

             
                string userName = selectedRow["UserName"].ToString();

           
                try
                {
                    using (SqlConnection connection = new SqlConnection("Data Source = localhost; Initial Catalog = User; Integrated Security = True"))
                    {
                        connection.Open();

                        // Создайте SQL-запрос для удаления пользователя
                        string query = $"DELETE FROM UserTable WHERE UserName = '{userName}'";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                  
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                 
                    //Console.WriteLine("Ошибка при удалении пользователя: " + ex.Message);
                }
            }
            else
            {
       
               // Console.WriteLine("Не выбран пользователь для удаления.");
            }


        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Close(); 
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            this.Close();
            EditUser edit = new EditUser();
            edit.Show();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {

        }
    }
}

