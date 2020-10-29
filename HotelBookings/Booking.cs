using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookings
{
    public class Booking
    {
        public Room RoomDetails { get; set; }
        public DateTime? BookingDate { get; set; }
        public string Guest { get; set; }

        public Booking()
        {
        }

        public Booking(string guest, Room room, DateTime bookingDate)
        {
            Guest = guest;
            RoomDetails = room;
            BookingDate = bookingDate;
        }
    }
}
