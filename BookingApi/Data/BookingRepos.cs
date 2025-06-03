using BookingApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookingApi.Data
{
    public class BookingRepos(BookingDbContext context)
    {
        private readonly BookingDbContext _context = context;

        public async Task<BookingEntity?> CreateBookingAsync(BookingEntity booking)
        {
            try
            {
                await _context.Bookings.AddAsync(booking);
                await _context.SaveChangesAsync();
                return booking;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in CreateBookingAsync: {ex.Message}");
                return null!;
            }
        }

        public async Task<List<BookingEntity>> GetAllAsync()
        {
            try
            {
                return await _context.Bookings.ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in GetAllAsync: {ex.Message}");
                return [];
            }
        }
    }
}
