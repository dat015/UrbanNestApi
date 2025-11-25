using UrbanNest.Services.Apartment.Domain.Common;

namespace UrbanNest.Services.Apartment.Domain.Entities;

public class Meter : BaseEntity
{
    public Guid TenantId { get; set; }
    public Guid? ApartmentId { get; set; }
    public string Serial { get; set; } = null!;
    public string MeterType { get; set; } = null!; // water, electric
    public DateTime? InstallDate { get; set; }
    public string? Metadata { get; set; }

    public virtual Apartment? Apartment { get; set; }
}