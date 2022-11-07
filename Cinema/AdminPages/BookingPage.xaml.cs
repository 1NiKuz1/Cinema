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
using static Cinema.AdminPages.SessionPage;

namespace Cinema.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для BookingPage.xaml
    /// </summary>
    public partial class BookingPage : Page
    {

        public class Booking
        {
            public int IdBooking { get; set; }
            public int IdSession { get; set; }
            public bool Status { get; set; }
            public string UserName { get; set; }
            public int SeatNumber { get; set; }
            public int RowNumber { get; set; }
            public int CodeToCheck { get; set; }
        }
        public BookingPage()
        {
            InitializeComponent();
            UpdateGrid();
        }

        public ObservableCollection<Session> Sessions;
        public ObservableCollection<Booking> Bookings;

        private void UpdateGrid()
        {
            var booking = (from p in SourceCore.MyBase.Booking
                           join s in SourceCore.MyBase.Seat on p.idSeat equals s.idSeat
                           join c in SourceCore.MyBase.Client on p.idClient equals c.idClient into outer
                           from itemO in outer.DefaultIfEmpty()
                           select new
                           {
                               IdBooking = p.idBooking,
                               IdSession = p.idSession,
                               Status = p.Status,
                               UserName = itemO.name,
                               SeatNumber = s.seatNumber,
                               RowNumber = s.rowNumber,
                               CodeToCheck = p.codeToCheck,
                           });
            Bookings = new ObservableCollection<Booking>();
            foreach (var item in booking)
            {
                Bookings.Add(new Booking
                {
                    IdBooking = item.IdBooking,
                    IdSession = item.IdSession,
                    Status = item.Status,
                    UserName = item.UserName,
                    SeatNumber = item.SeatNumber,
                    RowNumber = item.RowNumber,
                    CodeToCheck = (int)item.CodeToCheck
                });
            }
            BookingGrid.ItemsSource = Bookings;
        }

        private void BookingGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var s = (from p in SourceCore.MyBase.Session
                     join c in SourceCore.MyBase.Movie on p.idMovie equals c.idMovie
                     where p.idSession == ((Booking)BookingGrid.SelectedItem).IdSession
                     select new
                     {
                         IdSession = p.idSession,
                         Date = p.dateSession,
                         Time = p.sessionTime,
                         MovieName = c.movieName,
                         ChairPrice = p.costPerChair,
                         SofaPrice = p.costPerSofa
                     });
            Sessions = new ObservableCollection<Session>();
            foreach (var item in s)
            {
                Sessions.Add(new Session
                {
                    IdSession = item.IdSession,
                    Date = item.Date,
                    Time = item.Time,
                    MovieName = item.MovieName,
                    ChairPrice = item.ChairPrice,
                    SofaPrice = item.SofaPrice
                });
            }
            SessionGrid.ItemsSource = Sessions;
        }
    }
}
