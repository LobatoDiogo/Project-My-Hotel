using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class RoomRepository : IRoomRepository
    {
        protected readonly ITrybeHotelContext _context;
        public RoomRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 7. Refatore o endpoint GET /room
        public IEnumerable<RoomDto> GetRooms(int HotelId)
        {
            var hotel = _context.Hotels.First(h => h.HotelId == HotelId);

            var listRooms = _context.Rooms.Select(r => new RoomDto
            {

                RoomId = r.RoomId,
                Name = r.Name,
                Capacity = r.Capacity,
                Image = r.Image,
                Hotel = new HotelDto
                {
                    HotelId = hotel.HotelId,
                    Name = hotel.Name,
                    Address = hotel.Address,
                    CityId = hotel.CityId,
                    CityName = _context.Cities.First(c => c.CityId == hotel.CityId).Name,
                    State = _context.Cities.First(c => c.CityId == hotel.CityId).State
                }
            }).ToList();

            return listRooms;
        }

        // 8. Refatore o endpoint POST /room
        public RoomDto AddRoom(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();

            var hotelQuery = _context.Hotels
                .Where(h => h.HotelId == room.HotelId)
                .Select(h => new HotelDto
                {
                    HotelId = h.HotelId,
                    Name = h.Name,
                    Address = h.Address,
                    CityId = h.CityId,
                    CityName = _context.Cities
                        .Where(c => c.CityId == h.CityId)
                        .Select(c => c.Name)
                        .First(),
                    State = _context.Cities
                        .Where(c => c.CityId == h.CityId)
                        .Select(c => c.State)
                        .First()
                });

            var roomDto = new RoomDto
            {
                RoomId = room.RoomId,
                Name = room.Name,
                Capacity = room.Capacity,
                Image = room.Image,
                Hotel = hotelQuery.First()
            };

            return roomDto;
        }

        public void DeleteRoom(int RoomId)
        {
            var room = _context.Rooms.First(r => r.RoomId == RoomId);
            _context.Rooms.Remove(room);
            _context.SaveChanges();
        }
    }
}