using System;
using System.Collections.Generic;

namespace HavaYoluOtomasyonu.Models;

public partial class Aircraft
{
    public int AircraftId { get; set; }

    public int? ModelId { get; set; }

    public DateOnly? ManufactureDate { get; set; }

    public string TailNumber { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();

    public virtual ICollection<MaintenanceLog> MaintenanceLogs { get; set; } = new List<MaintenanceLog>();

    public virtual Model? Model { get; set; }
}
