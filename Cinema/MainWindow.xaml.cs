using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Base.cinemaEntities DataBase;
        public static Base.Client client = null;
        public MainWindow()
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
            if (client != null)
            {
                ShowUserStackPanel();
            }
            
        }

        private void ChangeWindow(string nameWindow)
        {
            switch (nameWindow)
            {
                case "AuthorizationWindow":
                    AuthorizationWindow authorizationWindow = new AuthorizationWindow();
                    Hide();
                    authorizationWindow.ShowDialog();
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

        private void ShowUserStackPanel()
        {
            UserStackPanel.Visibility = Visibility.Visible;
            NoNameStackPanel.Visibility = Visibility.Collapsed;
            NameUser.Text = client.name;
            if (client.isAdmin)
                AdminPanelButton.Visibility = Visibility.Visible;
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow("AuthorizationWindow");
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow("RegistrationWindow");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            client = null;
            UserStackPanel.Visibility = Visibility.Collapsed;
            NoNameStackPanel.Visibility = Visibility.Visible;
        }

        private void AdminPanel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
