using System.ComponentModel.DataAnnotations;

namespace BookingApi.Entities;

public class BookingEntity
{
    [Key]
    public int Id { get; set; }

    public string EventId { get; set; } = null!;
    public string UserEmail { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
