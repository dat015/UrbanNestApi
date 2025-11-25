namespace UrbanNest.Services.Apartment.Domain.Entities;

public class MeterReading
{
    // Không kế thừa BaseEntity vì dùng Time-series key
    public DateTime Time { get; set; }
    public Guid TenantId { get; set; }
    public Guid MeterId { get; set; }
    public decimal Value { get; set; }
    public short Quality { get; set; } = 0;
    public string? IngestionSource { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual Meter Meter { get; set; } = null!;
}