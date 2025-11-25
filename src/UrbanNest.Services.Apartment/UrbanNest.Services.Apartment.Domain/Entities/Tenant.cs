using UrbanNest.Services.Apartment.Domain.Common;

namespace UrbanNest.Services.Apartment.Domain.Entities;

public class Tenant : BaseEntity
{
    public string? Domain { get; set; }
    public string Timezone { get; set; } = "Asia/Ho_Chi_Minh";
    public string Status { get; set; } = "active";

    // Navigation Properties
    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public virtual ICollection<Building> Buildings { get; set; } = new List<Building>();
}