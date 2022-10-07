using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private Base.cinemaEntities DataBase;
        private double formGridHeight; 
        private double captchaGridHeight = 400;

        public RegistrationWindow(Base.cinemaEntities Database)
        {
            InitializeComponent();
            this.DataBase = Database;
        }

        private void ChangeWindow(string nameWindow)
        {
            switch (nameWindow)
            {
                case "MainWindow":
                    MainWindow mainWindow = new MainWindow();
                    Hide();
                    mainWindow.ShowDialog();
                    Close();
                    break;
                case "AuthorizationWindow":
                    AuthorizationWindow authorizationWindow = new AuthorizationWindow();
                    Hide();
                    authorizationWindow.ShowDialog();
                    Close();
                    break;
            }
        }
        private Boolean CheckPassword(String pas)
        {
            string pattern = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,24}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(pas);         
        }

        private Boolean CheckPhoneNumber(String phoneNumber)
        {
            string pattern = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(phoneNumber);
        }

        private void ShowAnotherGrid(Grid newGrid, Grid oldGrid, double heightNewGrid)
        {
            newGrid.Visibility = Visibility.Visible;
            newGrid.Height = heightNewGrid;
            oldGrid.Visibility = Visibility.Hidden;
            oldGrid.Height = 0;
        }

        private void FillCaptcha()
        {
            InputCaptchaTextBox.Text = "";
            string allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0";
            //разделитель
            char[] a = { ',' };
            //расщепление массива по разделителю
            String[] ar = allowchar.Split(a);
            String pwd = "";
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                string temp = ar[(r.Next(0, ar.Length))];
                pwd += temp;
            }
            CaptchaTextBox.Text = pwd;
        }

        private void CheckCaptcha_Click(object sender, RoutedEventArgs e)
        {
            ShowAnotherGrid(FormGrid, CaptchaGrid, formGridHeight);

            if (CaptchaTextBox.Text != InputCaptchaTextBox.Text)
            {
                MessageBox.Show("Неверна введена капча", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Base.Client client = new Base.Client
            {
                name = LoginText.Text,
                phoneNumber = PhoneNumberText.Text,
                isAdmin = false,
                password = PasswordBox.Password != "" ? PasswordBox.Password : PasswordTextBox.Text
            };
            // Добавление его в базу данных
            DataBase.Client.Add(client);
            // Сохранение изменений
            DataBase.SaveChanges();

            ChangeWindow("MainWindow");
        }


        private void RegistrationCommit_Click(object sender, RoutedEventArgs e)
        {

            // Создание и инициализация нового пользователя системы
            Boolean isPassword = PasswordBox.Password != "" ? CheckPassword(PasswordBox.Password) : CheckPassword(PasswordTextBox.Text);
            

            if (!isPassword)
            {
                MessageBox.Show("Неверный формат пароля, попробуйте еще раз", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!CheckPhoneNumber(PhoneNumberText.Text))
            {
                MessageBox.Show("Неверный формат номера телефона, попробуйте еще раз", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            formGridHeight = FormGrid.Height;
            ShowAnotherGrid(CaptchaGrid, FormGrid, captchaGridHeight);

            FillCaptcha();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow("AuthorizationWindow");
        }

        private void BackHome_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow("MainWindow");
        }

        private void PasswordButton_Click(object sender, RoutedEventArgs e)
        {
            // Переброска необходимой информации во временные буферы
            String Password = PasswordBox.Password;
            Visibility Visibility = PasswordBox.Visibility;
            double Width = PasswordBox.ActualWidth;
            // Изменение подписи на кнопке
            PasswordButton.Content = Visibility == Visibility.Visible ? "Скрыть" : "Показать";
            // Переброска информации из TextBox'а в PasswordBox
            PasswordBox.Password = PasswordTextBox.Text;
            PasswordBox.Visibility = PasswordTextBox.Visibility;
            PasswordBox.Width = PasswordTextBox.Width;
            // Возврат информации из временных буферов в TextBox
            PasswordTextBox.Text = Password;
            PasswordTextBox.Visibility = Visibility;
            PasswordTextBox.Width = Width;
        }
    }
}
