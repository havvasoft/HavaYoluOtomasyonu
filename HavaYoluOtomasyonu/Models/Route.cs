using System;
using System.Collections.Generic;

namespace HavaYoluOtomasyonu.Models;

public partial class Route
{
    public int RoutesId { get; set; }

    public int? DepartureAirportId { get; set; }

    public int? ArrivalAirportId { get; set; }

    public decimal? Distance { get; set; }

    public int? EstimatedDuration { get; set; }

    public virtual Airport? ArrivalAirport { get; set; }

    public virtual Airport? DepartureAirport { get; set; }

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
