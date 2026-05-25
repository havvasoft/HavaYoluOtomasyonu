using System;
using System.Collections.Generic;

namespace HavaYoluOtomasyonu.Models;

public partial class MaintenanceLog
{
    public int LogId { get; set; }

    public int? AircraftId { get; set; }

    public int? LeadEngineerId { get; set; }

    public string MaintenanceType { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly MaintenanceDate { get; set; }

    public virtual Aircraft? Aircraft { get; set; }

    public virtual Staff? LeadEngineer { get; set; }
}
