using System;
using System.Collections.Generic;

namespace HavaYoluOtomasyonu.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string? Department { get; set; }

    public string? SalaryGrade { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
