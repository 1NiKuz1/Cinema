using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.ActionsWithList
{
    public class ActionsWithTimeSessionItems
    {
        public static List<TimeSession> times = new List<TimeSession>() {
            new TimeSession { Time = "09:30" },
            new TimeSession { Time = "11:10" },
            new TimeSession { Time = "12:55" },
            new TimeSession { Time = "15:00" },
            new TimeSession { Time = "17:55" },
            new TimeSession { Time = "19:55" },
            new TimeSession { Time = "21:50" },
            new TimeSession { Time = "23:55" },
        };
        public class TimeSession
        {
            public string Time { get; set; }
        }

        public static List<string> ShowFilterTimeList(string dateText)
        {
            if (string.IsNullOrEmpty(dateText)) return GenerateTimeList();
            string[] date = dateText.Split('.');
            DateTime newDate = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
            var items = new List<string>();
            IEnumerable<Base.Session> sessionItems = SourceCore.MyBase.Session.ToList().Where(el => el.dateSession == newDate);
            bool f = true;
            foreach (TimeSession time in times)
            {
                foreach (Base.Session session in sessionItems)
                {
                    if (session.sessionTime.ToString().Substring(0, 5) == time.Time) f = false;
                }
                if (f) items.Add(time.Time);
                f = true;
            }
            return items;
        }

        public static List<string> GenerateTimeList()
        {
            List<string> items = new List<string>();
            foreach (TimeSession item in times)
            {
                items.Add(item.Time);
            }
            return items;
        }
    }
}
