using Cinema.ActionsWithList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для BookingWindow.xaml
    /// </summary>
    public partial class BookingWindow : Window
    {
        public BookingWindow()
        {
            InitializeComponent();
            SeattingInitialValues();
        }

        private void SeattingInitialValues()
        {
            if (MainWindow.client != null) PhoneNumberTextBox.Visibility = Visibility.Collapsed;
            SuccessText.Visibility = Visibility.Collapsed;
            SeatNumberText.Text = "Место: " + ActionsWithSeatItems.selectSeat.SeatNumber.ToString();
            RowNumberText.Text = "Ряд: " + ActionsWithSeatItems.selectSeat.RowNumber.ToString();
            PriceOfSeatText.Text = ActionsWithSeatItems.selectSeat.SeatType ?
                "Итого: " + ActionsWithSessionItems.selectSession.SofaPrice.ToString() :
                "Итого: " + ActionsWithSessionItems.selectSession.ChairPrice.ToString();
        }

        private int GetCodeFromPhone()
        {
            string code;
            if (MainWindow.client != null)
            {
                code = string.Join("", MainWindow.client.phoneNumber.Split('-'));
                return int.Parse(code.Substring(code.Length - 4));
            }
            code = string.Join("", PhoneNumberTextBox.Text.Split('-'));
            return int.Parse(code.Substring(code.Length - 4));
        }

        private void AddBooking(bool status)
        {
            SuccessText.Visibility = Visibility.Visible;
            SuccessText.Text = status ? "Место куплено" : "Место забронировано";
            Base.Booking booking = new Base.Booking
            {
                Status = status,
                idClient = MainWindow.client?.idClient,
                idSession = ActionsWithSessionItems.selectSession.IdSession,
                idSeat = ActionsWithSeatItems.selectSeat.IdSeat,
                codeToCheck = GetCodeFromPhone(),
            };
            // Добавление его в базу данных
            SourceCore.MyBase.Booking.Add(booking);
            // Сохранение изменений
            SourceCore.MyBase.SaveChanges();
            BookingButton.IsEnabled = false;
            BuyButton.IsEnabled = false;
        }

        private void BookingButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.client == null && !DataValidation.CheckPhoneNumber(PhoneNumberTextBox.Text))
            {
                MessageBox.Show("Неверный формат номера телефона, попробуйте еще раз", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            AddBooking(false);
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.client == null && !DataValidation.CheckPhoneNumber(PhoneNumberTextBox.Text))
            {
                MessageBox.Show("Неверный формат номера телефона, попробуйте еще раз", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            AddBooking(true);
        }

        private void BackHome_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.ChangeWindow("MainWindow", this);
        }
    }
}
