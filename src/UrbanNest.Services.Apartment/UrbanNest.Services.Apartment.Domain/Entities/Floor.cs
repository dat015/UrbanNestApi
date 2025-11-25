using UrbanNest.Services.Apartment.Domain.Common;

namespace UrbanNest.Services.Apartment.Domain.Entities;

public class Floor : BaseEntity
{
    public Guid BlockId { get; set; }
    public int Number { get; set; }

    public virtual Block Block { get; set; } = null!;
    public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
}