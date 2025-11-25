using UrbanNest.Services.Apartment.Domain.Common;

namespace UrbanNest.Services.Apartment.Domain.Entities;

public class InvoiceLine : BaseEntity
{
    public Guid InvoiceId { get; set; }
    public string? Description { get; set; }
    public decimal? Quantity { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? Tax { get; set; }
    public decimal? Total { get; set; }
    public string? Metadata { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;
}