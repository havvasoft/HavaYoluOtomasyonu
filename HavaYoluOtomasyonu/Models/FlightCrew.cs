using System;
using System.Collections.Generic;

namespace HavaYoluOtomasyonu.Models;

public partial class FlightCrew
{
    public int FlightCrewId { get; set; }

    public int? FlightId { get; set; }

    public int? StaffId { get; set; }

    public string AssigmentRole { get; set; } = null!;

    public virtual Flight? Flight { get; set; }

    public virtual Staff? Staff { get; set; }
}
