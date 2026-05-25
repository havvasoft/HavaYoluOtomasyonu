using System;
using System.Collections.Generic;

namespace HavaYoluOtomasyonu.Models;

public partial class Flight
{
    public int FlightId { get; set; }

    public int? RoutesId { get; set; }

    public int? AircraftId { get; set; }

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    public string? FlightStatus { get; set; }

    public virtual Aircraft? Aircraft { get; set; }

    public virtual ICollection<FlightCrew> FlightCrews { get; set; } = new List<FlightCrew>();

    public virtual ICollection<FlightStatusHistory> FlightStatusHistories { get; set; } = new List<FlightStatusHistory>();

    public virtual Route? Routes { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
