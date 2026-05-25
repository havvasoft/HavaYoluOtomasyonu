using System;
using System.Collections.Generic;

namespace HavaYoluOtomasyonu.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public string PnrCode { get; set; } = null!;

    public DateTime? BookingDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public int? CreatedByStaff { get; set; }

    public virtual Staff? CreatedByStaffNavigation { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
