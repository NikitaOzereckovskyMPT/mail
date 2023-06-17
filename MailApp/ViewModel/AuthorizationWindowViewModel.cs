using ImapX;
using ImapX.Collections;
using MailApp.Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MailApp.ViewModel
{
    public class AuthorizationWindowViewModel : BaseModel
    {
        private string _login;

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
                OnPropertyChanged(nameof(Connect));
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(Connect));
            }
        }

        private ComboBoxItem _item;

        public ComboBoxItem Item
        {
            get { return _item; }
            set { 
                _item = value;
                OnPropertyChanged(nameof(Item));
            }
        }



        public AuthorizationWindowViewModel()
        {

        }

        public RelayCommand Connect => new RelayCommand((obj) =>
        {
            var window = obj as Window;

            ImapHelper.Initialize(Item.Tag.ToString());

            if (ImapHelper.Login(Login, Password))
            {
               
                MainWindow mainWindow = new MainWindow();
                window.Close();
                mainWindow.Show();
            }
            else
                MessageBox.Show("Неудачная попытка авторизации!");

        }, (obj) => !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password));
    }
}
