using UrbanNest.Services.Apartment.Domain.Common;

namespace UrbanNest.Services.Apartment.Domain.Entities;

public class Ticket : BaseEntity
{
    public Guid TenantId { get; set; }
    public Guid? ApartmentId { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? AssignedTo { get; set; }
    public string Status { get; set; } = "open";
    public string? Priority { get; set; } = "normal";
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Images { get; set; } // JSONB array
    public string? Classification { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual Apartment? Apartment { get; set; }
    public virtual User? Creator { get; set; }
    public virtual User? Assignee { get; set; }
}