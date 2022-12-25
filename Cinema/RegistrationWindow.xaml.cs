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

        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void ShowAnotherGrid(Grid showGrid, Grid hideGrid)
        {
            showGrid.Visibility = Visibility.Visible;
            hideGrid.Visibility = Visibility.Collapsed;
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
            ShowAnotherGrid(FormGrid, CaptchaGrid);

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
            SourceCore.MyBase.Client.Add(client);
            // Сохранение изменений
            SourceCore.MyBase.SaveChanges();

            MainWindow.client = client;

            WindowManager.ChangeWindow("MainWindow", this);
        }

        private void RegistrationCommit_Click(object sender, RoutedEventArgs e)
        {

            // Создание и инициализация нового пользователя системы
            Boolean isPassword = PasswordBox.Password != "" ? DataValidation.CheckPassword(PasswordBox.Password) : DataValidation.CheckPassword(PasswordTextBox.Text);

            if (!DataValidation.CheckUserExist(LoginText.Text))
            {
                MessageBox.Show("Пользователь уже существует", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!isPassword)
            {
                MessageBox.Show("Пароль должен состоять и латиницы, содержать от 8 до 24 символов, а также не менее одной заглавной буквы и цифры", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!DataValidation.CheckPhoneNumber(PhoneNumberText.Text))
            {
                MessageBox.Show("Неверный формат номера телефона, попробуйте еще раз", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ShowAnotherGrid(CaptchaGrid, FormGrid);

            FillCaptcha();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.ChangeWindow("AuthorizationWindow", this);
        }

        private void BackHome_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.ChangeWindow("MainWindow", this);
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
