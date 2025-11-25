using UrbanNest.Services.Apartment.Domain.Common;

namespace UrbanNest.Services.Apartment.Domain.Entities;

public class Vehicle : BaseEntity
{
    public Guid TenantId { get; set; }
    public string Plate { get; set; } = null!;
    public Guid? OwnerId { get; set; }
    public string? VehicleType { get; set; }
    public DateTime? AllowedFrom { get; set; }
    public DateTime? AllowedTo { get; set; }
    public string? Metadata { get; set; }

    public virtual User? Owner { get; set; }
}