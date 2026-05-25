using System;
using System.Collections.Generic;

namespace HavaYoluOtomasyonu.Models;

public partial class Airport
{
    public int AirportId { get; set; }

    public string AirportName { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string Iatacode { get; set; } = null!;

    public string? TimeZone { get; set; }

    public virtual ICollection<Gate> Gates { get; set; } = new List<Gate>();

    public virtual ICollection<Route> RouteArrivalAirports { get; set; } = new List<Route>();

    public virtual ICollection<Route> RouteDepartureAirports { get; set; } = new List<Route>();
}
