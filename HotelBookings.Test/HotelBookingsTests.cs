using NUnit.Framework;
using System;
using System.Linq;
using System.Xml.XPath;

namespace HotelBookings.Test
{
    public class HotelBookingsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GivenAGuestThenShouldAddBooking()
        {
            // ARRANGE
            var bookingManager = new BookingManager();
            var bookingDate = new DateTime(2012, 3, 28);

            // ACT
            bookingManager.AddBooking("Patel", 101, bookingDate);

            // ASSERT
            var bookingCount = bookingManager
                .Bookings
                .Where(x => x.Guest == "Patel" && x.RoomDetails.RoomNumber == 101 && x.BookingDate == bookingDate)
                .Count();           

            Assert.AreEqual(1, bookingCount);
        }

        [Test]
        public void GivenABookingWhenRoomAvailableThenReturnTrue()
        {
            // ARRANGE
            var bookingManager = new BookingManager();
            var bookingDate = new DateTime(2012, 3, 28);

            // ACT
            var result = bookingManager.IsRoomAvailable(101, bookingDate);

            // ASSERT
            Assert.True(result);
        }

        [Test]
        public void GivenABookingWhenRoomNotAvailableThenReturnFalse()
        {
            // ARRANGE
            var bookingManager = new BookingManager();
            var bookingDate = new DateTime(2012, 3, 28);

            // ACT
            bookingManager.AddBooking("Patel", 101, bookingDate);
            var result = bookingManager.IsRoomAvailable(101, bookingDate);

            // ASSERT
            Assert.False(result);
        }

        [Test]
        public void GivenBookingsWhenAddingDuplicateThenThrowException()
        {
            // ARRANGE
            var bookingManager = new BookingManager();
            var bookingDate = new DateTime(2012, 3, 28);

            // ACT
            bookingManager.AddBooking("Patel", 101, bookingDate);

            // ASSERT
            var ex = Assert.Throws<Exception>(() => bookingManager.AddBooking("Li", 101, bookingDate));
        }

        [Test]
        public void GivenBookingsShouldReturnCorrectAmountOfBookings()
        {
            // ARRANGE
            var bookingManager = new BookingManager();
            var bookingDate = new DateTime(2012, 3, 28);

            // ACT
            bookingManager.AddBooking("Patel", 101, bookingDate);
            bookingManager.AddBooking("Halford", 102, bookingDate);

            // ASSERT
            var result = bookingManager.GetAvailableRooms(bookingDate);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result.Where(x => x == 103).Count());
            Assert.AreEqual(1, result.Where(x => x == 104).Count());
        }
    }
}