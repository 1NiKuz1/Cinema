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
            if (client != null)
            {
                ShowUserStackPanel();
            }
            DateList.ItemsSource = ActionsWithDateItems.dateItems;
            DateList.SelectedIndex = 0;
            SeatList.ItemsSource = ActionsWithSeatItems.sessionItems;
            //ActionsWithPictures.PutImageBase64InDb("movie_2.jpg", 2);
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
                    RegistrationWindow registrationWindow = new RegistrationWindow(SourceCore.MyBase);
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
            }
            else
            {
                HallPanel.Visibility = Visibility.Collapsed;
            }
        }
                

        private void SeatList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

//ObservableCollection<SessionItem> sessionItems;
//List<DateItem> dateItems;

//public class SessionItem
//{
//    public string ImagePath { get; set; }
//    public string TimeAndPrice { get; set; }
//    public string MovieName { get; set; }
//    public string AgeRestriction { get; set; }
//    public string Duration { get; set; }
//    public string Tags { get; set; }
//    public DateTime Date { get; set; }
//}
//public class DateItem
//{
//    public string DayOfWeek { get; set; }
//    public string Day { get; set; }
//    public string Month { get; set; }
//    public DateTime Date { get; set; }
//}

//private ObservableCollection<SessionItem> ShowSessionList() {
//    ObservableCollection<SessionItem> items = new ObservableCollection<SessionItem>();
//    DateItem date = (DateItem)DateList.SelectedItem;
//    foreach (SessionItem item in sessionItems)
//    {
//        if (date.Date == item.Date)
//            items.Add(item);
//    }
//    return items;
//}

//private ObservableCollection<SessionItem> GenerateSessionList()
//{
//    ObservableCollection<SessionItem> items = new ObservableCollection<SessionItem>();
//    List<Base.Session> sessions = SourceCore.MyBase.Session.ToList();
//    foreach (Base.Session item in sessions)
//    {
//        //ActionsWithPictures.GetBase64ImageFromDb(item.idMovie);
//        Base.Movie movie = SourceCore.MyBase.Movie.SingleOrDefault(U => U.idMovie == item.idMovie);
//        string imagePath = $"{ActionsWithPictures.pathImages}movie_{item.idMovie}";
//        string timeAndPrice = $"{item.sessionTime} {item.costPerChair} Руб";
//        string movieName = $"{movie.movieName}";
//        string ageRestriction = $"Возрастное ограничение: {movie.ageRestriction}+";
//        string duration = $"Длительность: {movie.duration}";
//        string tags = $"{movie.tags}";
//        items.Add(new SessionItem() { ImagePath = imagePath, TimeAndPrice = timeAndPrice, MovieName = movieName, AgeRestriction = ageRestriction, Duration = duration, Tags = tags, Date = item.dateSession });
//    }
//    return items;
//}

//private List<DateItem> GenerateDateList()
//{
//    DateTime dateTime = DateTime.Today;
//    List<DateItem> items = new List<DateItem>();
//    for (int i = 0; i < 7; i++)
//    {
//        string dayOfWeek = dateTime.ToString("dddd");
//        dayOfWeek = dayOfWeek.Substring(0, 1).ToUpper() + dayOfWeek.Substring(1);
//        string month = dateTime.ToString("MMMM");
//        string day = dateTime.Day.ToString();
//        items.Add(new DateItem() { DayOfWeek = dayOfWeek, Day = day, Month = month, Date = dateTime });
//        dateTime = dateTime.AddDays(1);
//    } 
//    return items;
//}