using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.ActionsWithList
{
    internal class ActionsWithDateItems
    {
        public static List<DateItem> dateItems = GenerateDateList();

        public class DateItem
        {
            public string DayOfWeek { get; set; }
            public string Day { get; set; }
            public string Month { get; set; }
            public DateTime Date { get; set; }
        }

        private static List<DateItem> GenerateDateList()
        {
            //DateTime dateTime = DateTime.Today;
            DateTime dateTime = new DateTime(2022, 10, 29);
            List<DateItem> items = new List<DateItem>();
            for (int i = 0; i < 7; i++)
            {
                string dayOfWeek = dateTime.ToString("dddd");
                dayOfWeek = dayOfWeek.Substring(0, 1).ToUpper() + dayOfWeek.Substring(1);
                string month = dateTime.ToString("MMMM");
                string day = dateTime.Day.ToString();
                items.Add(new DateItem() { DayOfWeek = dayOfWeek, Day = day, Month = month, Date = dateTime });
                dateTime = dateTime.AddDays(1);
            }
            return items;
        }
    }
}
