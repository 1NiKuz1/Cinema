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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void MovieButton_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new AdminPages.MoviePage());
        }

        private void SessionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BookingButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClientButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackHomeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.ChangeWindow("MainWindow", this);
        }
    }
}
