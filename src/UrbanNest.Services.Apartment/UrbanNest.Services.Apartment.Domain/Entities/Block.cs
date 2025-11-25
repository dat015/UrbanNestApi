using System.Drawing;
using UrbanNest.Services.Apartment.Domain.Common;

namespace UrbanNest.Services.Apartment.Domain.Entities;

public class Block : BaseEntity
{
    public Guid BuildingId { get; set; }
    public string Name { get; set; } = null!;

    public virtual Building Building { get; set; } = null!;
    public virtual ICollection<Floor> Floors { get; set; } = new List<Floor>();
}