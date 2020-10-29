using HotelBookings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelBookings
{
    public class BookingManager : IBookingManager
    {
        public List<Booking> Bookings { get; set; }
        private readonly List<Room> _rooms;

        public BookingManager()
        {
            Bookings = new List<Booking>();

            _rooms = new List<Room>()
            { 
                new Room(){ RoomNumber = 101},
                new Room(){ RoomNumber = 102},
                new Room(){ RoomNumber = 103},
                new Room(){ RoomNumber = 104},
            };
        }
        

        public void AddBooking(string guest, int roomNumber, DateTime date)
        {
            if (!IsRoomAvailable(roomNumber, date))
                throw new Exception("Room is already booked!");

            var room = _rooms.Where(x => x.RoomNumber == roomNumber).FirstOrDefault();

            Bookings.Add(new Booking(guest, room, date));
        }
        
        public bool IsRoomAvailable(int roomNumber, DateTime date)
        {
            var count = Bookings.Where(x => x.RoomDetails.RoomNumber == roomNumber && x.BookingDate == date).Count();

            if (count == 0)
                return true;

            return false;
        }

        public IEnumerable<int> GetAvailableRooms(DateTime date)
        {
            var availableRooms = new List<int>();

            var bookingsForDate = Bookings.Where(x => x.BookingDate == date.Date).ToList();

            foreach (var room in _rooms)
            {
                if (!bookingsForDate.Any(x => x.RoomDetails.RoomNumber == room.RoomNumber))
                {
                    availableRooms.Add(room.RoomNumber);
                }
            }

            return availableRooms;
        }
    }
}
