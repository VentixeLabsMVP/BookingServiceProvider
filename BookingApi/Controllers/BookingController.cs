using Azure.Core;
using BookingApi.Entities;
using BookingApi.Models;
using BookingApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "BookingToken")] // this will be demanidng the token later
    public class BookingController(BookingServvie bookingService) : ControllerBase
    {
        private readonly BookingServvie _bookingService = bookingService;


        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequestDto request)
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            Console.WriteLine("Claims:");
            foreach (var claim in claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
                return Unauthorized();

            var booking = new BookingEntity
            {
                UserEmail = userEmail,
                EventId = request.EventId
            };

            var result = await _bookingService.CreateBookingAsync(booking);
            if (result == null)
                return BadRequest("Booking failed");

            return Ok(result);
        }
    }
}
