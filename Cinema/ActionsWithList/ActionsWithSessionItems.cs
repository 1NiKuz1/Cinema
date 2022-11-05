using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cinema.ActionsWithList.ActionsWithDateItems;

namespace Cinema.ActionsWithList
{
    public class ActionsWithSessionItems
    {
        public static ObservableCollection<SessionItem> sessionItems = GenerateSessionList();
        public static SessionItem selectSession = null;

        public class SessionItem
        {
            public int IdSession { get; set; }
            public int IdMovie { get; set; }
            public string ImagePath { get; set; }
            public string Time { get; set; }
            public int ChairPrice { get; set; }
            public int SofaPrice { get; set; }
            public string MovieName { get; set; }
            public string AgeRestriction { get; set; }
            public string Duration { get; set; }
            public string Tags { get; set; }
            public DateTime Date { get; set; }
        }

        public static void SetSelectSession(object selectItem)
        {
            selectSession = (SessionItem)selectItem;
        }

        public static ObservableCollection<SessionItem> ShowSessionList(object date)
        {
            List<SessionItem> items = new List<SessionItem>();
            DateItem dateBuf = date as DateItem;
            foreach (SessionItem item in sessionItems)
            {
                if (dateBuf.Date == item.Date)
                    items.Add(item);
            }
            items.Sort((x, y) => x.Time.CompareTo(y.Time));
            return new ObservableCollection<SessionItem>(items);
        }

        public static void UpdateImages()
        {
            List<Base.Session> sessions = SourceCore.MyBase.Session.ToList();
            foreach (Base.Session item in sessions)
            {
                ActionsWithPictures.GetBase64ImageFromDb(item.idMovie);
            }
        }

        public static ObservableCollection<SessionItem> GenerateSessionList()
        {
            ObservableCollection<SessionItem> items = new ObservableCollection<SessionItem>();
            List<Base.Session> sessions = SourceCore.MyBase.Session.ToList();
            foreach (Base.Session item in sessions)
            {
                Base.Movie movie = SourceCore.MyBase.Movie.SingleOrDefault(U => U.idMovie == item.idMovie);
                string imagePath = $"{ActionsWithPictures.pathImages}movie_{item.idMovie}.jpg";
                string time = item.sessionTime.ToString().Substring(0, 5) + " ";
                string movieName = $"{movie.movieName}";
                string ageRestriction = $"{movie.ageRestriction}+";
                string duration = $"{movie.duration} мин";
                string tags = $"{movie.tags}";
                items.Add(new SessionItem() {
                    ImagePath = imagePath,
                    Time = time,
                    ChairPrice = item.costPerChair,
                    SofaPrice = item.costPerSofa,
                    MovieName = movieName,
                    AgeRestriction = ageRestriction,
                    Duration = duration,
                    Tags = tags,
                    Date = item.dateSession,
                    IdSession = item.idSession,
                    IdMovie = item.idMovie,
                });
            }
            return items;
        }
    }
}
