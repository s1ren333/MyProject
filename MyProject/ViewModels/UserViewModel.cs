using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MyProject.Models;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using System.Data;
using System.Xml.Linq;
using System.Windows.Media;
using MyProject.View;
using System.Windows.Controls;

namespace MyProject.ViewModels 
{

    public class UserViewModel : INotifyPropertyChanged
    {

        private string connectionString = "Data Source=localhost;Initial Catalog=User;Integrated Security=True";
        private string username;
        private string password;
        private User newUser;

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public UserViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            NewUser = new User();
            AddUserCommand = new RelayCommand(AddUser);
            EditUserCommand = new RelayCommand(EditUser);
        }
        public User NewUser
        {
            get { return newUser; }
            set
            {
                newUser = value;
                OnPropertyChanged(nameof(NewUser));
            }
        }

        public ICommand LoginCommand { get; private set; }
        public ICommand AddUserCommand { get; private set; }
        public ICommand EditUserCommand { get; private set; }


        private void Login()
        {
            Main m = new Main();
            MainWindow mainWindow = new MainWindow();
            if (IsUserValid(Username, Password))
            {
                App.Current.Windows[0].Close();
                MessageBox.Show($"Добро пожаловать {Username}"); 
                m.Show();
            }
            else
            {
                MessageBox.Show("Пользователь не найден в базе данных.");
            }
        }
    
        private bool IsUserValid(string username, string password)
        {           
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM UserTable WHERE UserName = @UserName AND Password = @Password", connection);
                    command.Parameters.AddWithValue("@UserName", username);
                    command.Parameters.AddWithValue("@Password", password);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }

                catch (Exception ex)
                {
                    return false;
                }
            }           
        }

        private void AddUser()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                

                SqlCommand command = new SqlCommand("INSERT INTO UserTable (FirstName, LastName, Username, Password) VALUES (@FirstName, @LastName, @Username, @Password)", connection);
                try
                {

                command.Parameters.AddWithValue("@FirstName", NewUser.FirstName);
                command.Parameters.AddWithValue("@LastName", NewUser.LastName);
                command.Parameters.AddWithValue("@Username", NewUser.Username);
                command.Parameters.AddWithValue("@Password", NewUser.Password);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Пользователь успешно добавлен в базу данных.");
                }
                else
                {
                    MessageBox.Show("Не удалось добавить пользователя в базу данных.");
                }
                }
                catch 
                {
                    MessageBox.Show("Введены некорректные данные или такой пользователь существует", "Некорректные данные", MessageBoxButton.OKCancel, MessageBoxImage.None);

                }
            }
        }

        private void EditUser()
        {
            App.Current.Windows[0].Close();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"UPDATE UserTable SET FirstName = @FirstName, LastName = @LastName, Password = @Password WHERE UserName = @UserName", connection);
                try
                {
                    command.Parameters.AddWithValue("@UserName", NewUser.Username);
                    command.Parameters.AddWithValue("@FirstName", NewUser.FirstName);
                    command.Parameters.AddWithValue("@LastName", NewUser.LastName);
                   
                    command.Parameters.AddWithValue("@Password", NewUser.Password);

                    int rowAffected = command.ExecuteNonQuery();

                    if (rowAffected > 0)
                    {
                        MessageBox.Show("Редактирование прошло успешно");                      
                    }
                }
                catch
                {
                    MessageBox.Show("Пользователь не отредактирован");
                }
                connection.Close();
            }
        }
        // Реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }     
    }
    
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }
        public void Execute(object parameter)
        {
            _execute?.Invoke();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }   
}
