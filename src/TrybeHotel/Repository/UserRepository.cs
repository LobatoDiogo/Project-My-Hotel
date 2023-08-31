using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly ITrybeHotelContext _context;
        public UserRepository(ITrybeHotelContext context)
        {
            _context = context;
        }
        public UserDto GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public UserDto Login(LoginDto login)
        {
            var userLogin = _context.Users.FirstOrDefault(u => u.Email == login.Email && u.Password == login.Password);

            if (userLogin == null)
            {
                return null!;
            }

            var dto = new UserDto
            {
                UserId = userLogin.UserId,
                Name = userLogin.Name,
                Email = userLogin.Email,
                UserType = userLogin.UserType
            };

            return dto;
        }
        public UserDto Add(UserDtoInsert user)
        {
            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                UserType = "client"
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            var dto = new UserDto
            {
                UserId = newUser.UserId,
                Name = newUser.Name,
                Email = newUser.Email,
                UserType = newUser.UserType
            };

            return dto;
        }

        public UserDto GetUserByEmail(string userEmail)
        {
            var hasUser = _context.Users.FirstOrDefault(u => u.Email == userEmail);

            if (hasUser == null)
            {
                return null!;
            }

            var dto = new UserDto
            {
                UserId = hasUser.UserId,
                Name = hasUser.Name,
                Email = hasUser.Email,
                UserType = hasUser.UserType
            };

            return dto;
        }

        public IEnumerable<UserDto> GetUsers()
        {
            throw new NotImplementedException();
        }

    }
}