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

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        private Base.cinemaEntities DataBase;
        public AuthorizationWindow()
        {
            InitializeComponent();
            try
            {
                DataBase = new Base.cinemaEntities();
            }
            catch
            {
                MessageBox.Show("Не удалось подключиться к базе данных. Проверьте настройки подключения приложения.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                Close();
            }
        }

        private void ChangeWindow(string nameWindow)
        {
            switch (nameWindow)
            {
                case "MainWindow":
                    MainWindow mainWindow = new MainWindow();
                    Hide();
                    mainWindow.ShowDialog();
                    Close();
                    break;
                case "RegistrationWindow":
                    RegistrationWindow registrationWindow = new RegistrationWindow(DataBase);
                    Hide();
                    registrationWindow.ShowDialog();
                    Close();
                    break;
            }
        }

        private void BackHome_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow("MainWindow");
        }

        private void AuthorizationCommit_Click(object sender, RoutedEventArgs e)
        {
            Base.Client User = DataBase.Client.SingleOrDefault(U => U.name == LoginText.Text && U.password == PasswordText.Text);
            if (User != null)
            {
                MainWindow.client = User;
                ChangeWindow("MainWindow");
            }
            else
            {
                MessageBox.Show("Неверно указан логин и/или пароль!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            };
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow("RegistrationWindow");
        }
    }
}
