using UrbanNest.Services.Apartment.Domain.Common;

namespace UrbanNest.Services.Apartment.Domain.Entities;

public class Invoice : BaseEntity
{
    public Guid TenantId { get; set; }
    public Guid? ApartmentId { get; set; }
    public string InvoiceNo { get; set; } = null!;
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public decimal AmountTotal { get; set; }
    public string Currency { get; set; } = "VND";
    public string Status { get; set; } = "draft";
    public bool IsLeakSuspected { get; set; } = false;
    public string? Metadata { get; set; }
    public DateTime? IssuedAt { get; set; }

    public virtual Apartment? Apartment { get; set; }
    public virtual ICollection<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();
}