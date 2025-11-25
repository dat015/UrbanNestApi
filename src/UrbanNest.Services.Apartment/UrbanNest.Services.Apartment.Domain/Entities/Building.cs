using UrbanNest.Services.Apartment.Domain.Common;

namespace UrbanNest.Services.Apartment.Domain.Entities;

public class Building : BaseEntity
{
    public Guid TenantId { get; set; }
    public string Name { get; set; } = null!;
    public string? Address { get; set; }
    public string? Metadata { get; set; } // Map JSONB -> string

    public virtual Tenant Tenant { get; set; } = null!;
    public virtual ICollection<Block> Blocks { get; set; } = new List<Block>();
}