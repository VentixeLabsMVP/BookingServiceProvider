using BookingApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingApi.Data;

public class BookingDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<BookingEntity> Bookings => Set<BookingEntity>();
}
