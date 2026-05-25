using System;
using System.Collections.Generic;

namespace HavaYoluOtomasyonu.Models;

public partial class Gate
{
    public int GateId { get; set; }

    public int? AirportId { get; set; }

    public string GateNumber { get; set; } = null!;

    public string? Terminal { get; set; }

    public virtual Airport? Airport { get; set; }
}
