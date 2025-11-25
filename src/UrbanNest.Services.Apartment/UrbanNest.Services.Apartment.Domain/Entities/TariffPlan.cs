using UrbanNest.Services.Apartment.Domain.Common;

namespace UrbanNest.Services.Apartment.Domain.Entities;

public class TariffPlan : BaseEntity
{
    public Guid TenantId { get; set; }
    public string Name { get; set; } = null!;
    public string Target { get; set; } = null!; // water, electric
    public string CalculationStrategy { get; set; } = null!;
    public string? Config { get; set; } // JSONB
    public DateTime? EffectiveFrom { get; set; }
    public DateTime? EffectiveTo { get; set; }
}