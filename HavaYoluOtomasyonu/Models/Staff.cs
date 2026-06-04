using System;
using System.Collections.Generic;

namespace HavaYoluOtomasyonu.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Password { get; set; }

    public DateOnly HireDate { get; set; }

    public int? RoleId { get; set; }

    public string? EmploymentStatus { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<FlightCrew> FlightCrews { get; set; } = new List<FlightCrew>();

    public virtual ICollection<MaintenanceLog> MaintenanceLogs { get; set; } = new List<MaintenanceLog>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<TechnicalCertification> TechnicalCertifications { get; set; } = new List<TechnicalCertification>();
}
