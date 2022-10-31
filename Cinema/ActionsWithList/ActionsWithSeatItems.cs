using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cinema.ActionsWithList.ActionsWithSessionItems;

namespace Cinema.ActionsWithList
{
    internal class ActionsWithSeatItems
    {
        const int SIZE_SEAT = 30;
        const string COLOR_FREE = "#F7D7AE";
        const string COLOR_LOCK = "#D75E55";
        public static ObservableCollection<SeatItem> sessionItems = GenerateSeatList();
        public static SeatItem selectSeat = null;

        public class SeatItem
        {
            public int IdSeat { get; set; }
            public int SeatWidth { get; set; }
            public int SeatHeight { get; set; }
            public string SeatFill { get; set; }
            public bool SeatStatus { get; set; }
            public bool SeatType { get; set; }
            public int SeatNumber { get; set; }
            public int RowNumber { get; set; }
        }

        public static void SetSelectSeat(object selectItem)
        {
            selectSeat = (SeatItem)selectItem;
        }

        public static bool StatusCheck(object selectItem)
        {
            selectSeat = (SeatItem)selectItem;
            return selectSeat.SeatStatus;
        }
        public static ObservableCollection<SeatItem> FilterLockAndFreeSeatList(object itemSession)
        {
            SessionItem bufItemSession = itemSession as SessionItem;
            IEnumerable<Base.Booking> seatIdItems = SourceCore.MyBase.Booking.ToList().Where(el => el.idSession == bufItemSession.IdSession);
            ObservableCollection<SeatItem> items = new ObservableCollection<SeatItem>();
            List<Base.Seat> seats = SourceCore.MyBase.Seat.ToList();
            foreach (Base.Seat item in seats)
            {
                int width = item.type ? SIZE_SEAT * 3 : SIZE_SEAT;
                string color = COLOR_FREE;
                bool status = true;
                foreach (Base.Booking itemBooking in seatIdItems)
                {
                    if (itemBooking.idSeat == item.idSeat) {
                        status = false;
                        color = COLOR_LOCK;
                    }
                }
                items.Add(new SeatItem() {
                    IdSeat = item.idSeat,
                    SeatWidth = width,
                    SeatHeight = SIZE_SEAT,
                    SeatFill = color,
                    SeatStatus = status,
                    SeatType = item.type,
                    SeatNumber = item.seatNumber,
                    RowNumber = item.rowNumber
                });
            }
            return items;
        }
        
        private static ObservableCollection<SeatItem> GenerateSeatList()
        {
            ObservableCollection<SeatItem> items = new ObservableCollection<SeatItem>();
            List<Base.Seat> seats = SourceCore.MyBase.Seat.ToList();
            foreach (Base.Seat item in seats)
            {
                int width = item.type ? SIZE_SEAT * 3 : SIZE_SEAT;
                items.Add(new SeatItem() {
                    IdSeat = item.idSeat,
                    SeatWidth = width,
                    SeatHeight = SIZE_SEAT,
                    SeatFill = COLOR_FREE,
                    SeatStatus = true,
                    SeatType = item.type,
                    SeatNumber = item.seatNumber,
                    RowNumber = item.rowNumber,
                });
            }
            return items;
        }
    }
}



//public static void FilterLockAndFreeSeatList(object itemSession)
//{
//    SessionItem bufItemSession = itemSession as SessionItem;
//    if (bufItemSession == null) return;
//    IEnumerable<Base.Booking> seatIdItems = SourceCore.MyBase.Booking.ToList().Where(el => el.idSession == bufItemSession.IdSession);
//    foreach (Base.Booking item in seatIdItems)
//    {
//        int index = sessionItems.First(el => el.IdSeat == item.idSeat).IdSeat;
//        if (index != -1)
//            sessionItems[index].SeatFill = COLOR_LOCK;
//    }
//}