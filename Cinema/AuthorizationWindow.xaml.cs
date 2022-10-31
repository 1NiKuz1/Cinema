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
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        

        private void BackHome_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.ChangeWindow("MainWindow", this);
        }

        private void AuthorizationCommit_Click(object sender, RoutedEventArgs e)
        {
            Base.Client User = SourceCore.MyBase.Client.SingleOrDefault(U => U.name == LoginText.Text && U.password == PasswordText.Text);
            if (User != null)
            {
                MainWindow.client = User;
                WindowManager.ChangeWindow("MainWindow", this);
            }
            else
            {
                MessageBox.Show("Неверно указан логин и/или пароль!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            };
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.ChangeWindow("RegistrationWindow", this);
        }
    }
}
