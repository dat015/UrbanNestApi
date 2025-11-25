using UrbanNest.Services.Apartment.Domain.Common;

namespace UrbanNest.Services.Apartment.Domain.Entities;

public class ParkingEvent : BaseEntity
{
    public Guid TenantId { get; set; }
    public string? CameraId { get; set; }
    public string? Plate { get; set; }
    public DateTime RecognizedAt { get; set; } = DateTime.UtcNow;
    public Guid? MatchedVehicleId { get; set; }
    public string? Action { get; set; } // enter/exit
    public string? GateId { get; set; }
    public string? RawPayload { get; set; } // JSONB

    public virtual Vehicle? MatchedVehicle { get; set; }
}