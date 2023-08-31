using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class CityRepository : ICityRepository
    {
        protected readonly ITrybeHotelContext _context;
        public CityRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 4. Refatore o endpoint GET /city
        public IEnumerable<CityDto> GetCities()
        {
            var listCities = _context.Cities.Select(c => new CityDto
            {
                CityId = c.CityId,
                Name = c.Name,
                State = c.State
            }).ToList();

            return listCities;
        }

        // 2. Refatore o endpoint POST /city
        public CityDto AddCity(City city)
        {
            _context.Cities.Add(city);
            _context.SaveChanges();

            var dto = new CityDto
            {
                CityId = city.CityId,
                Name = city.Name,
                State = city.State
            };

            return dto;
        }

        // 3. Desenvolva o endpoint PUT /city
        public CityDto UpdateCity(City city)
        {
            var cityUpdate = _context.Cities.FirstOrDefault(c => c.CityId == city.CityId);

            if (cityUpdate == null)
            {
                return null!;
            }

            cityUpdate.Name = city.Name;
            cityUpdate.State = city.State;

            _context.SaveChanges();

            var dto = new CityDto
            {
                CityId = cityUpdate.CityId,
                Name = cityUpdate.Name,
                State = cityUpdate.State
            };

            return dto;
        }

    }
}