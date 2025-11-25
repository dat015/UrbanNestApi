using UrbanNest.Services.Apartment.Domain.Common;

namespace UrbanNest.Services.Apartment.Domain.Entities;

public class User : BaseEntity
{
    //liên kết với Identity
    public Guid IdentityUserId { get; set; }
    public Guid TenantId { get; set; }
    public string Email { get; set; } = null!;
    public string? FullName { get; set; }
    public string Role { get; set; } = null!; // admin, resident...
    public string? Phone { get; set; }
    public string? Metadata { get; set; }
    public virtual Tenant Tenant { get; set; } = null!;
}