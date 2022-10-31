using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cinema
{
    internal class WindowManager
    {
        public static void ChangeWindow(string nameWindow, Window currentWindow)
        {
            switch (nameWindow)
            {
                case "MainWindow":
                    MainWindow mainWindow = new MainWindow();
                    currentWindow.Hide();
                    mainWindow.ShowDialog();
                    currentWindow.Close();
                    break;
                case "RegistrationWindow":
                    RegistrationWindow registrationWindow = new RegistrationWindow();
                    currentWindow.Hide();
                    registrationWindow.ShowDialog();
                    currentWindow.Close();
                    break;
                case "AuthorizationWindow":
                    AuthorizationWindow authorizationWindow = new AuthorizationWindow();
                    currentWindow.Hide();
                    authorizationWindow.ShowDialog();
                    currentWindow.Close();
                    break;
                case "BookingWindow":
                    BookingWindow bookingWindow = new BookingWindow();
                    currentWindow.Hide();
                    bookingWindow.ShowDialog();
                    currentWindow.Close();
                    break;
            }
        }
    }
}
