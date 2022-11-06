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
using static Cinema.ActionsWithList.ActionsWithSessionItems;

namespace Cinema.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для SessionPage.xaml
    /// </summary>
    public partial class SessionPage : Page
    {
        public class Session
        {
            public int IdSession { get; set; }
            public DateTime Date { get; set; }
            public TimeSpan Time { get; set; }
            public string MovieName { get; set; }
            public int ChairPrice { get; set; }
            public int SofaPrice { get; set; }
        }
        public SessionPage()
        {
            InitializeComponent();
            DataContext = this;
            UpdateGrid(null);
            RecordComboBoxTime.ItemsSource = times;
            movies = GenerateMoviesList();
            RecordComboBoxMovieName.ItemsSource = movies;
            dates = GenerateDateList();
            RecordComboBoxDate.ItemsSource = dates;
            DlgLoad(false, "");
        }

        private int DlgMode = 0;
        public Session SelectedItem;
        public List<string> times = new List<string>() { "09:30", "11:10", "12:55", "15:00", "17:55", "19:55", "21:50", "23:55" };
        public List<string> movies;
        public List<string> dates;
        public ObservableCollection<Session> Sessions;

        private List<string> ShowFilterTimeList(string dateText)
        {
            if (string.IsNullOrEmpty(dateText)) return times;
            string[] date = dateText.Split('.');
            DateTime newDate = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
            var items = new List<string>();
            IEnumerable<Base.Session> sessionItems = SourceCore.MyBase.Session.ToList().Where(el => el.dateSession == newDate);
            bool f = true;
            foreach (string time in times)
            {
                foreach (Base.Session session in sessionItems)
                {
                    if (session.sessionTime.ToString().Substring(0, 5) == time) f = false;
                }
                if (f) items.Add(time);
                f = true;
            }
            return items;
        }

        private static List<string> GenerateDateList()
        {
            List<string> items = new List<string>();
            foreach (ActionsWithList.ActionsWithDateItems.DateItem item in ActionsWithList.ActionsWithDateItems.dateItems)
            {
                items.Add(item.Date.ToString("dd.MM.yyyy"));
            }
            return items;
        }

        private List<string> GenerateMoviesList()
        {
            List<string> items = new List<string>();
            foreach(Base.Movie item in SourceCore.MyBase.Movie)
            {
                items.Add(item.movieName);
            }
            return items;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> Columns = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                Columns.Add(PageGrid.Columns[i].Header.ToString());
            }
            FilterComboBox.ItemsSource = Columns;
            FilterComboBox.SelectedIndex = 0;

            foreach (DataGridColumn Column in PageGrid.Columns)
            {
                Column.CanUserSort = false;
            }
        }

        private void UpdateGrid(Session Session)
        {
            if ((Session == null) && (PageGrid.ItemsSource != null))
            {
                Session = (Session)PageGrid.SelectedItem;
            }
            var s = (from p in SourceCore.MyBase.Session
                                            join c in SourceCore.MyBase.Movie on p.idMovie equals c.idMovie
                                            select new { 
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
                Sessions.Add(new Session { 
                    IdSession = item.IdSession,
                    Date = item.Date,
                    Time = item.Time,
                    MovieName = item.MovieName,
                    ChairPrice = item.ChairPrice,
                    SofaPrice = item.SofaPrice
                });
            }
            PageGrid.ItemsSource = Sessions;
            PageGrid.SelectedItem = Session;
            RecordComboBoxTime.ItemsSource = times;
            sessionItems = GenerateSessionList();
        }

        private void DlgLoad(bool b, string DlgModeContent)
        {
            if (b == true)
            {
                ColumnChange.Width = new GridLength(300);
                PageGrid.IsHitTestVisible = false;
                RecordLabel.Content = DlgModeContent + " запись";
                AddCommit.Content = DlgModeContent;
                RecordAdd.IsEnabled = false;
                RecordCopy.IsEnabled = false;
                RecordEdit.IsEnabled = false;
                RecordDellete.IsEnabled = false;
            }
            else
            {
                ColumnChange.Width = new GridLength(0);
                PageGrid.IsHitTestVisible = true;
                RecordAdd.IsEnabled = true;
                RecordCopy.IsEnabled = true;
                RecordEdit.IsEnabled = true;
                RecordDellete.IsEnabled = true;
                DlgMode = -1;
            }
        }

        private void FillTextBox()
        {
            RecordComboBoxTime.ItemsSource = ShowFilterTimeList((string)RecordComboBoxDate.SelectedItem);
            RecordComboBoxDate.SelectedItem = SelectedItem.Date.ToString("dd.MM.yyyy");
            RecordComboBoxTime.SelectedItem = null;
            RecordComboBoxMovieName.SelectedItem = movies.First(movie => movie == SelectedItem.MovieName);
            RecordTextChairPrice.Text = SelectedItem.ChairPrice.ToString();
            RecordTextSofaPrice.Text = SelectedItem.SofaPrice.ToString();
        }

        private void RecordAdd_Click(object sender, RoutedEventArgs e)
        {
            DlgLoad(true, "Добавить");
            DataContext = null;
            DlgMode = 0;
        }

        private void RecordkCopy_Click(object sender, RoutedEventArgs e)
        {
            if (PageGrid.SelectedItem != null)
            {
                DlgLoad(true, "Копировать");
                SelectedItem = (Session)PageGrid.SelectedItem;
                FillTextBox();
                DlgMode = 0;
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
            }
        }

        private void RecordEdit_Click(object sender, RoutedEventArgs e)
        {
            if (PageGrid.SelectedItem != null)
            {
                DlgLoad(true, "Изменить");
                SelectedItem = (Session)PageGrid.SelectedItem;
                FillTextBox();
            }
            else
            {
                MessageBox.Show("Не выбрано ни одной строки!", "Сообщение", MessageBoxButton.OK);
            }
        }

        private void RecordDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {

                try
                {
                    // Ссылка на удаляемую книгу
                    Base.Session Deleting = SourceCore.MyBase.Session.First(item => item.idSession == ((Session)PageGrid.SelectedItem).IdSession);
                    // Определение ссылки, на которую должен перейти указатель после удаления
                    if (PageGrid.SelectedIndex < PageGrid.Items.Count - 1)
                    {
                        PageGrid.SelectedIndex++;
                    }
                    else
                    {
                        if (PageGrid.SelectedIndex > 0)
                        {
                            PageGrid.SelectedIndex--;
                        }
                    }
                    Session SelectingItem = (Session)PageGrid.SelectedItem;
                    SourceCore.MyBase.Session.Remove(Deleting);
                    SourceCore.MyBase.SaveChanges();
                    UpdateGrid(SelectingItem);
                }
                catch
                {

                    MessageBox.Show("Невозможно удалить запись, так как она используется в других справочниках базы данных.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None);
                }
            }
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            switch (FilterComboBox.SelectedIndex)
            {
                case 0:
                    PageGrid.ItemsSource = Sessions.Where(q => q.Date.ToString("dd.MM.yyyy").Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    PageGrid.ItemsSource = Sessions.Where(q => q.Time.ToString().Substring(0, 5).Contains(textbox.Text)).ToList();
                    break;
                case 2:
                    PageGrid.ItemsSource = Sessions.Where(q => q.MovieName.Contains(textbox.Text)).ToList();
                    break;
                case 3:
                    PageGrid.ItemsSource = Sessions.Where(q => q.ChairPrice.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 4:
                    PageGrid.ItemsSource = Sessions.Where(q => q.SofaPrice.ToString().Contains(textbox.Text)).ToList();
                    break;
            }
        }

        private void AddCommit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(RecordTextChairPrice.Text))
                errors.AppendLine("Укажите цену кресла");

            if (string.IsNullOrEmpty(RecordTextSofaPrice.Text))
                errors.AppendLine("Укажите цену дивана");

            if (string.IsNullOrEmpty((string)RecordComboBoxMovieName.SelectedItem))
                errors.AppendLine("Укажите название фильма");

            if (string.IsNullOrEmpty((string)RecordComboBoxDate.SelectedItem))
                errors.AppendLine("Укажите дату");

            if (string.IsNullOrEmpty((string)RecordComboBoxTime.SelectedItem))
                errors.AppendLine("Укажите выберите время");


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (DlgMode == 0)
            {
                string[] time = ((string)RecordComboBoxTime.SelectedItem).Split(':');
                string[] date = ((string)RecordComboBoxDate.SelectedItem).Split('.');
                var NewBase = new Base.Session();
                NewBase.dateSession = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
                NewBase.sessionTime = new TimeSpan(int.Parse(time[0]), int.Parse(time[1]), 0);
                NewBase.idMovie = SourceCore.MyBase.Movie.First(p => p.movieName == (string)RecordComboBoxMovieName.SelectedItem).idMovie;
                NewBase.costPerChair = int.Parse(RecordTextChairPrice.Text);
                NewBase.costPerSofa = int.Parse(RecordTextSofaPrice.Text);
                SourceCore.MyBase.Session.Add(NewBase);
                //SelectedItem = NewBase;
            }
            else
            {
                var EditBase = new Base.Session();
                EditBase = SourceCore.MyBase.Session.First(p => p.idSession == SelectedItem.IdSession);
                string[] time = ((string)RecordComboBoxTime.SelectedItem).Split(':');
                string[] date = ((string)RecordComboBoxDate.SelectedItem).Split('.');
                EditBase.dateSession = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
                EditBase.sessionTime = new TimeSpan(int.Parse(time[0]), int.Parse(time[1]), 0);
                EditBase.idMovie = SourceCore.MyBase.Movie.First(p => p.movieName == (string)RecordComboBoxMovieName.SelectedItem).idMovie;
                EditBase.costPerChair = int.Parse(RecordTextChairPrice.Text);
                EditBase.costPerSofa = int.Parse(RecordTextSofaPrice.Text);
            }

            try
            {
                SourceCore.MyBase.SaveChanges();
                UpdateGrid(SelectedItem);
                DlgLoad(false, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void AddRollback_Click(object sender, RoutedEventArgs e)
        {
            UpdateGrid(SelectedItem);
            DlgLoad(false, "");
        }

        private void RecordComboBoxDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RecordComboBoxTime.ItemsSource = ShowFilterTimeList((string)RecordComboBoxDate.SelectedItem);
        }
    }
}