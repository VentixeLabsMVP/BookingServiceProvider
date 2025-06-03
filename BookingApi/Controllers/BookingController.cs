using BookingApi.Entities;
using BookingApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // this will be demanidng the token later
    public class BookingController(BookingServvie bookingService) : ControllerBase
    {
        private readonly BookingServvie _bookingService = bookingService;


        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] string eventId)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
                return Unauthorized();

            var booking = new BookingEntity
            {
                UserEmail = userEmail,
                EventId = eventId
            };

            var result = await _bookingService.CreateBookingAsync(booking);
            if (result == null)
                return BadRequest("Booking failed");

            return Ok(result);
        }
    }
}
