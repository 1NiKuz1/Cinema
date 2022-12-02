using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cinema
{
    internal class DataValidation
    {
        public static bool CheckPassword(string pas)
        {
            string pattern = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,24}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(pas);
        }

        public static bool CheckUserExist(string name) {
            return SourceCore.MyBase.Client.FirstOrDefault(cl => cl.name == name) == null;
        }

        public static bool CheckPhoneNumber(string phoneNumber)
        {
            string pattern = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(phoneNumber);
        }

    }
}
