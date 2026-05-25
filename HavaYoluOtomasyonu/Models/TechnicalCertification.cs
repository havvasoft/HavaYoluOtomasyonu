using System;
using System.Collections.Generic;

namespace HavaYoluOtomasyonu.Models;

public partial class TechnicalCertification
{
    public int CertificationId { get; set; }

    public int? StaffId { get; set; }

    public int? ModelId { get; set; }

    public string LicenseType { get; set; } = null!;

    public DateOnly ExpiryDate { get; set; }

    public virtual Model? Model { get; set; }

    public virtual Staff? Staff { get; set; }
}
