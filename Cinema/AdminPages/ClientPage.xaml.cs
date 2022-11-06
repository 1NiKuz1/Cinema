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
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public ClientPage()
        {
            InitializeComponent();
            DataContext = this;
            UpdateGrid(null);
            DlgLoad(false, "");
            RecordComboBoxIsAdmin.ItemsSource = new List<bool>() { true, false };
        }

        private int DlgMode = 0;
        public Base.Client SelectedItem;
        public ObservableCollection<Base.Client> Clients;


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

        private void UpdateGrid(Base.Client Client)
        {
            if ((Client == null) && (PageGrid.ItemsSource != null))
            {
                Client = (Base.Client)PageGrid.SelectedItem;
            }
            Clients = new ObservableCollection<Base.Client>(SourceCore.MyBase.Client);
            PageGrid.ItemsSource = Clients;
            PageGrid.SelectedItem = Client;
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
            RecordTextName.Text = SelectedItem.name;
            RecordTextPhoneNumber.Text = SelectedItem.phoneNumber;
            RecordComboBoxIsAdmin.SelectedItem = SelectedItem.isAdmin;
            RecordTextPassword.Text = SelectedItem.password;
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
                SelectedItem = (Base.Client)PageGrid.SelectedItem;
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
                SelectedItem = (Base.Client)PageGrid.SelectedItem;
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
                    Base.Client DeletingAccessory = (Base.Client)PageGrid.SelectedItem;
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
                    Base.Client SelectingAccessory = (Base.Client)PageGrid.SelectedItem;
                    SourceCore.MyBase.Client.Remove(DeletingAccessory);
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
                    PageGrid.ItemsSource = SourceCore.MyBase.Client.Where(q => q.name.Contains(textbox.Text)).ToList();
                    break;
                case 1:
                    PageGrid.ItemsSource = SourceCore.MyBase.Client.Where(q => q.phoneNumber.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 2:
                    PageGrid.ItemsSource = SourceCore.MyBase.Client.Where(q => q.isAdmin.ToString().Contains(textbox.Text)).ToList();
                    break;
                case 3:
                    PageGrid.ItemsSource = SourceCore.MyBase.Client.Where(q => q.password.ToString().Contains(textbox.Text)).ToList();
                    break;
            }
        }

        private void AddCommit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(RecordTextName.Text))
                errors.AppendLine("Укажите имя пользователя");

            if (!DataValidation.CheckPhoneNumber(RecordTextPhoneNumber.Text))
                errors.AppendLine("некоректно указан номер телефона пользователя");

            if (RecordComboBoxIsAdmin.SelectedItem == null)
                errors.AppendLine("Укажите возратсное ограничение");

            if (string.IsNullOrEmpty(RecordTextPassword.Text))
                errors.AppendLine("Укажите пароль пользователя");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (DlgMode == 0)
            {
                var NewBase = new Base.Client();
                NewBase.name = RecordTextName.Text.Trim();
                NewBase.phoneNumber = RecordTextPhoneNumber.Text;
                NewBase.isAdmin = (bool)RecordComboBoxIsAdmin.SelectedItem;
                NewBase.password = RecordTextPassword.Text.Trim();
                SourceCore.MyBase.Client.Add(NewBase);
                SelectedItem = NewBase;
            }
            else
            {
                var EditBase = new Base.Client();
                EditBase = SourceCore.MyBase.Client.First(p => p.idClient == SelectedItem.idClient);
                EditBase.name = RecordTextName.Text.Trim();
                EditBase.phoneNumber = RecordTextPhoneNumber.Text;
                EditBase.isAdmin = (bool)RecordComboBoxIsAdmin.SelectedItem;
                EditBase.password = RecordTextPassword.Text.Trim();
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
    }
}
