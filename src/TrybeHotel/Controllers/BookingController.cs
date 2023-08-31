using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TrybeHotel.Dto;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("booking")]

    public class BookingController : Controller
    {
        private readonly IBookingRepository _repository;
        public BookingController(IBookingRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "Client")]
        public IActionResult Add([FromBody] BookingDtoInsert bookingInsert)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var insertBooking = _repository.Add(bookingInsert, email);

            if (insertBooking == null)
            {
                return BadRequest(new { message = "Guest quantity over room capacity" });
            }

            return Created("", insertBooking);
        }


        [HttpGet("{Bookingid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "Client")]
        public IActionResult GetBooking(int Bookingid)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var booking = _repository.GetBooking(Bookingid, email);

            if (booking == null)
            {
                return Unauthorized();
            }

            return Ok(booking);
        }
    }
}