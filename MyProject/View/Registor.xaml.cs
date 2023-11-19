using MyProject.ViewModels;
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

namespace MyProject.View
{
    /// <summary>
    /// Логика взаимодействия для Registor.xaml
    /// </summary>
    public partial class Registor : Window
    {
        public Registor()
        {
            InitializeComponent();
            DataContext = new UserViewModel();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(id_user.Text == "" && FirstName.Text == "" && LastName.Text == "" && UserName.Text == "" && Password.Text == "")
            {
                MessageBox.Show("Вы ввели некоренктные данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }
    }
}
