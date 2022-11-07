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

namespace Cinema.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для MoviePage.xaml
    /// </summary>
    public partial class MoviePage : Page
    {
        public MoviePage()
        {
            InitializeComponent();
            DataContext = this;
            UpdateGrid(null);
            DlgLoad(false, "");
        }

        private int DlgMode = 0;
        public Base.Movie SelectedItem;
        public ObservableCollection<Base.Movie> Movies;


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> Columns = new List<string>();
            for (int i = 0; i < 4; i++)
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

        private void UpdateGrid(Base.Movie Movie)
        {
            if ((Movie == null) && (PageGrid.ItemsSource != null))
            {
                Movie = (Base.Movie)PageGrid.SelectedItem;
            }
            Movies = new ObservableCollection<Base.Movie>(SourceCore.MyBase.Movie);
            PageGrid.ItemsSource = Movies;
            PageGrid.SelectedItem = Movie;
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
            RecordTextMovieName.Text = SelectedItem.movieName;
            RecordTextDuration.Text = SelectedItem.duration.ToString();
            RecordAgeRestriction.Text = SelectedItem.ageRestriction.ToString();
            RecordTextTags.Text = SelectedItem.tags;
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
                SelectedItem = (Base.Movie)PageGrid.SelectedItem;
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
                SelectedItem = (Base.Movie)PageGrid.SelectedItem;
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
                    Base.Movie DeletingAccessory = (Base.Movie)PageGrid.SelectedItem;
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
                    Base.Movie SelectingAccessory = (Base.Movie)PageGrid.SelectedItem;
                    SourceCore.MyBase.Movie.Remove(DeletingAccessory);
                    SourceCore.MyBase.SaveChanges();
                    UpdateGrid(SelectingAccessory);
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
                    PageGrid.ItemsSource = SourceCore.MyBase.Movie.Where(q => q.movieName.Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    PageGrid.ItemsSource = SourceCore.MyBase.Movie.Where(q => q.duration.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 2:
                    PageGrid.ItemsSource = SourceCore.MyBase.Movie.Where(q => q.ageRestriction.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 3:
                    PageGrid.ItemsSource = SourceCore.MyBase.Movie.Where(q => q.tags.ToString().Contains(textbox.Text)).ToList();
                    break;
            }
        }

        private void AddCommit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(RecordTextMovieName.Text))
                errors.AppendLine("Укажите название фильма");

            if (string.IsNullOrEmpty(RecordTextDuration.Text))
                errors.AppendLine("Укажите продолжительность");

            if (string.IsNullOrEmpty(RecordAgeRestriction.Text))
                errors.AppendLine("Укажите возратсное ограничение");

            if (string.IsNullOrEmpty(RecordTextTags.Text))
                errors.AppendLine("Укажите метки");

            if (string.IsNullOrEmpty(RecordTextScreen.Text))
                errors.AppendLine("Укажите название картинки");

            string[] buf = RecordTextScreen.Text.Split('.');
            if (buf[buf.Length - 1] != "jpg")
                errors.AppendLine("Укажите название картинки");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (DlgMode == 0)
            {
                try
                {
                    var NewBase = new Base.Movie();
                    NewBase.movieName = RecordTextMovieName.Text.Trim();
                    NewBase.duration = int.Parse(RecordTextDuration.Text);
                    NewBase.ageRestriction = int.Parse(RecordAgeRestriction.Text);
                    NewBase.tags = RecordTextTags.Text.Trim();
                    NewBase.screen = ActionsWithPictures.ConsertImageToBinary(RecordTextScreen.Text);
                    SourceCore.MyBase.Movie.Add(NewBase);
                    SelectedItem = NewBase;
                }
                catch (Exception)
                {
                    MessageBox.Show("Введены некоректные данные");
                }
            }
            else
            {
                try
                {
                    var EditBase = new Base.Movie();
                    EditBase = SourceCore.MyBase.Movie.First(p => p.idMovie == SelectedItem.idMovie);
                    EditBase.movieName = RecordTextMovieName.Text.Trim();
                    EditBase.duration = int.Parse(RecordTextDuration.Text);
                    EditBase.ageRestriction = int.Parse(RecordAgeRestriction.Text);
                    EditBase.tags = RecordTextTags.Text.Trim();
                    EditBase.screen = ActionsWithPictures.ConsertImageToBinary(RecordTextScreen.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Введены некоректные данные");
                }
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

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                RecordTextScreen.Text = openFileDialog.FileName;
            }
        }
    }
}
