using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class BookingRepository : IBookingRepository
    {
        protected readonly ITrybeHotelContext _context;
        public BookingRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 9. Refatore o endpoint POST /booking
        public BookingResponse Add(BookingDtoInsert booking, string email)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.RoomId == booking.RoomId);
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            var hotel = _context.Hotels.FirstOrDefault(h => h.HotelId == room!.HotelId);
            var city = _context.Cities.FirstOrDefault(c => c.CityId == hotel!.CityId);

            if (room == null || booking.GuestQuant > room.Capacity)
            {
                return null!;
            }

            var newBooking = new Booking
            {
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                GuestQuant = booking.GuestQuant,
                Room = room,
            };

            _context.Bookings.Add(newBooking);
            _context.SaveChanges();

            var bookingResponse = new BookingResponse
            {
                BookingId = newBooking.BookingId,
                CheckIn = newBooking.CheckIn,
                CheckOut = newBooking.CheckOut,
                GuestQuant = newBooking.GuestQuant,
                Room = new RoomDto
                {
                    RoomId = room.RoomId,
                    Name = room.Name,
                    Capacity = room.Capacity,
                    Image = room.Image,
                    Hotel = new HotelDto
                    {
                        HotelId = hotel!.HotelId,
                        Name = hotel.Name,
                        Address = hotel.Address,
                        CityId = hotel.CityId,
                        CityName = city!.Name,
                        State = city!.State
                    }
                }
            };

            return bookingResponse;
        }

        // 10. Refatore o endpoint GET /booking
        public BookingResponse GetBooking(int bookingId, string email)
        {
            var book = _context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            var room = _context.Rooms.FirstOrDefault(r => r.RoomId == book!.RoomId);
            var hotel = _context.Hotels.FirstOrDefault(h => h.HotelId == room!.HotelId);
            var city = _context.Cities.FirstOrDefault(c => c.CityId == hotel!.CityId);
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (book == null || book.UserId != user!.UserId)
            {
                return null!;
            }

            var bookingResponse = new BookingResponse
            {
                BookingId = book.BookingId,
                CheckIn = book.CheckIn,
                CheckOut = book.CheckOut,
                GuestQuant = book.GuestQuant,
                Room = new RoomDto
                {
                    RoomId = room!.RoomId,
                    Name = room.Name,
                    Capacity = room.Capacity,
                    Image = room.Image,
                    Hotel = new HotelDto
                    {
                        HotelId = hotel!.HotelId,
                        Name = hotel.Name,
                        Address = hotel.Address,
                        CityId = hotel.CityId,
                        CityName = city!.Name,
                        State = city!.State
                    }
                }
            };

            return bookingResponse;
        }

        public Room GetRoomById(int RoomId)
        {
            throw new NotImplementedException();
        }

    }

}