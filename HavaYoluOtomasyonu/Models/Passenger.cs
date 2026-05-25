using System;
using System.Collections.Generic;

namespace HavaYoluOtomasyonu.Models;

public partial class Passenger
{
    public int PassengerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string PassportNumber { get; set; } = null!;

    public string? Phone { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Nationality { get; set; }

    public string? LoyaltyProgramId { get; set; }

    public string? EmergencyContact { get; set; }

    public string? SpecialNeeds { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
