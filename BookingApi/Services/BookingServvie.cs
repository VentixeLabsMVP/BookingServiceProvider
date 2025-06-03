using BookingApi.Data;
using BookingApi.Entities;

namespace BookingApi.Services
{
    public class BookingServvie(BookingDbContext context)
    {
        private readonly BookingDbContext _context = context;
        // only add for now. repos is rdy for getall
        public async Task<BookingEntity?> CreateBookingAsync(BookingEntity booking)
        {
            if (booking == null)
                return null;

            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
            return booking;
        }
    }
}
