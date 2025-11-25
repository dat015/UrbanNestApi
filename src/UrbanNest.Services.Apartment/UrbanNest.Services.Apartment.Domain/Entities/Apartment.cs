using UrbanNest.Services.Apartment.Domain.Common;

namespace UrbanNest.Services.Apartment.Domain.Entities;

public class Apartment : BaseEntity
{
    public Guid FloorId { get; set; }
    public Guid TenantId { get; set; }
    public string Code { get; set; } = null!; // VD: A-12-05
    public Guid? OwnerId { get; set; }
    public decimal? AreaM2 { get; set; }
    public string? UnitType { get; set; }
    public string? Metadata { get; set; }

    public virtual Floor Floor { get; set; } = null!;
    public virtual User? Owner { get; set; }
}