using System;
using System.Collections.Generic;

namespace HavaYoluOtomasyonu.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public string TicketNumber { get; set; } = null!;

    public int? FlightId { get; set; }

    public int? PassengerId { get; set; }

    public string? SeatNumber { get; set; }

    public string ClassType { get; set; } = null!;

    public int? BookingId { get; set; }

    public int? FareId { get; set; }

    public bool? IsCheckedIn { get; set; }

    public int? BaggageAllowance { get; set; }

    public virtual ICollection<Baggage> Baggages { get; set; } = new List<Baggage>();

    public virtual Booking? Booking { get; set; }

    public virtual Fare? Fare { get; set; }

    public virtual Flight? Flight { get; set; }

    public virtual Passenger? Passenger { get; set; }
}
