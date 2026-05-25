using System;
using System.Collections.Generic;

namespace HavaYoluOtomasyonu.Models;

public partial class Fare
{
    public int FareId { get; set; }

    public string FareBasicCode { get; set; } = null!;

    public bool Refundable { get; set; }

    public bool Changeable { get; set; }

    public string CabinType { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
