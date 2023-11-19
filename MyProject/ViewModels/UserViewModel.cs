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

namespace MyProject.ViewModels 
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private string connectionString = "Data Source=localhost;Initial Catalog=User;Integrated Security=True";
        private string username;
        private string password;
        private int id;
        private User newUser;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

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

        public ICommand LoginCommand { get; private set; }

        public UserViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            NewUser = new User();
            AddUserCommand = new RelayCommand(AddUser);
        }

        private void Login()
        {
            if (IsUserValid(Username, Password))
            {
                MessageBox.Show($"Добро пожаловать {Username}"); 
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
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM user_table1 WHERE UserName = @UserName AND Password = @Password", connection);
                    command.Parameters.AddWithValue("@UserName", username);
                    command.Parameters.AddWithValue("@Password", password);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            
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

        public ICommand AddUserCommand { get; private set; }


        private void AddUser()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("INSERT INTO user_table1 (Id_user, FirstName, LastName, Username, Password) VALUES (@Id, @FirstName, @LastName, @Username, @Password)", connection);
                command.Parameters.AddWithValue("@Id", NewUser.Id);
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
