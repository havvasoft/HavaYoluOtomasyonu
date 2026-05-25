using System;
using System.Collections.Generic;

namespace HavaYoluOtomasyonu.Models;

public partial class Model
{
    public int ModelId { get; set; }

    public string Manufacturer { get; set; } = null!;

    public string ModelName { get; set; } = null!;

    public string IcaoTypeCode { get; set; } = null!;

    public int CapacityEconomy { get; set; }

    public int CapacityBusiness { get; set; }

    public decimal? AvgSpeed { get; set; }

    public virtual ICollection<Aircraft> Aircraft { get; set; } = new List<Aircraft>();

    public virtual ICollection<TechnicalCertification> TechnicalCertifications { get; set; } = new List<TechnicalCertification>();
}
