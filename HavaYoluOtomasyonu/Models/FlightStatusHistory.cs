using System;
using System.Collections.Generic;

namespace HavaYoluOtomasyonu.Models;

public partial class FlightStatusHistory
{
    public int HistoryId { get; set; }

    public int? FlightId { get; set; }

    public string Status { get; set; } = null!;

    public string? Reason { get; set; }

    public DateTime? UpdateTime { get; set; }

    public virtual Flight? Flight { get; set; }
}
