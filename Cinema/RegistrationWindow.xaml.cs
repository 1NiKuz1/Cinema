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
        public RegistrationWindow(Base.cinemaEntities Database)
        {
            InitializeComponent();
            this.DataBase = Database;
        }

        private String CheckPassword(String pas)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{8,24}$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(pas))
            {
                return pas;
            }
            return "";
        }

        private void CheckCaptcha_Click(object sender, RoutedEventArgs e)
        {
            FormGrid.Visibility = Visibility.Visible;
            FormGrid.Height = formGridHeight;
            CaptchaGrid.Visibility = Visibility.Hidden;
            CaptchaGrid.Height = 0;
            //string allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            //allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            //allowchar += "1,2,3,4,5,6,7,8,9,0";
            ////разделитель
            //char[] a = { ',' };
            ////расщепление массива по разделителю
            //String[] ar = allowchar.Split(a);
            //String pwd = " ";
            //Random r = new Random();
            //for (int i = 0; i < 6; i++)
            //{
            //    string temp = ar[(r.Next(0, ar.Length))];
            //    pwd += temp;
            //}
            //CaptchaTextBox.Text = pwd;
            
        }


        private void RegistrationCommit_Click(object sender, RoutedEventArgs e)
        {
            CaptchaGrid.Visibility = Visibility;
            CaptchaGrid.Height = 144;
            FormGrid.Visibility = Visibility.Hidden;
            formGridHeight = FormGrid.Height;
            FormGrid.Height = 0;

            // Создание и инициализация нового пользователя системы
            //String password = PasswordBox.Password != "" ? CheckPassword(PasswordBox.Password) : CheckPassword(PasswordTextBox.Text);
            //if (password != "")
            //{
            //    Base.Client client = new Base.Client();
            //    client.name = LoginText.Text;
            //    client.password = PasswordBox.Password != "" ? PasswordBox.Password : PasswordTextBox.Text;
            //    // Добавление его в базу данных
            //    DataBase.Client.Add(client);
            //    // Сохранение изменений
            //    DataBase.SaveChanges();
            //}
            //else
            //{
            //    MessageBox.Show("Неверный формат пароля, попробуйте еще раз", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            //};
            //Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow window = new AuthorizationWindow();
            window.ShowDialog();
        }

        private void BackHome_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из программы?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                Close();
            }
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
