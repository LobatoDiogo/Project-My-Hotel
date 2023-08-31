using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class HotelRepository : IHotelRepository
    {
        protected readonly ITrybeHotelContext _context;
        public HotelRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        //  5. Refatore o endpoint GET /hotel
        public IEnumerable<HotelDto> GetHotels()
        {
            var listHotels = _context.Hotels.Select(h => new HotelDto
            {
                HotelId = h.HotelId,
                Name = h.Name,
                Address = h.Address,
                CityId = h.CityId,
                CityName = _context.Cities.Where(c => c.CityId == h.CityId).Select(c => c.Name).FirstOrDefault(),
                State = _context.Cities.Where(c => c.CityId == h.CityId).Select(c => c.State).FirstOrDefault()
            }).ToList();

            return listHotels;
        }

        // 6. Refatore o endpoint POST /hotel
        public HotelDto AddHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            _context.SaveChanges();

            var dto = new HotelDto
            {
                HotelId = hotel.HotelId,
                Name = hotel.Name,
                Address = hotel.Address,
                CityId = hotel.CityId,
                CityName = _context.Cities.Where(c => c.CityId == hotel.CityId).Select(c => c.Name).FirstOrDefault(),
                State = _context.Cities.Where(c => c.CityId == hotel.CityId).Select(c => c.State).FirstOrDefault()
            };

            return dto;
        }
    }
}