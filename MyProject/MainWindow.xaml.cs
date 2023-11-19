using MyProject.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new UserViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Registor r = new Registor();
            r.Show();
            Close();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Window1 f = new Window1();
            f.Show();
        }

        private void Login_Click_1(object sender, RoutedEventArgs e)
        {
            if(UserNameText.Text == "" && PasswordText.Text == "")
            {
                MessageBox.Show("Внимание вы не ввели данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                Main m = new Main();
                m.Show();
                Close();
            }
        }
        

    }
}
