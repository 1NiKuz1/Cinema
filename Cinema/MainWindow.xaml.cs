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
using Cinema.ActionsWithList;

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Base.Client client = null;
        
        public MainWindow()
        {
            InitializeComponent();
            //client = new Base.Client
            //{
            //    name = "admin",
            //    phoneNumber = "79877547829",
            //    isAdmin = true,
            //    password = "admin"
            //};
            if (client != null)
            {
                ShowUserStackPanel();
            }
            DateList.ItemsSource = ActionsWithDateItems.dateItems;
            DateList.SelectedIndex = 0;
            SeatList.ItemsSource = ActionsWithSeatItems.sessionItems;
            ActionsWithSessionItems.UpdateImages();
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
            WindowManager.ChangeWindow("AuthorizationWindow", this);
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.ChangeWindow("RegistrationWindow", this);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            client = null;
            UserStackPanel.Visibility = Visibility.Collapsed;
            NoNameStackPanel.Visibility = Visibility.Visible;
        }

        private void AdminPanel_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.ChangeWindow("AdminWindow", this);
        }

        private void DateList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SessionList.ItemsSource = ActionsWithSessionItems.ShowSessionList(DateList.SelectedItem);
        }

        private void SessionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SessionList.SelectedItem != null)
            {
                HallPanel.Visibility = Visibility.Visible;
                SeatList.ItemsSource = ActionsWithSeatItems.FilterLockAndFreeSeatList(SessionList.SelectedItem);
                ActionsWithSessionItems.SetSelectSession(SessionList.SelectedItem);
            }
            else
            {
                HallPanel.Visibility = Visibility.Collapsed;
            }
        }
                

        private void SeatList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!ActionsWithSeatItems.StatusCheck(SeatList.SelectedItem)) return;
            ActionsWithSeatItems.SetSelectSeat(SeatList.SelectedItem);
            WindowManager.ChangeWindow("BookingWindow", this);
        }
    }
}