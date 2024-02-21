using System;
using System.Collections.Generic;

namespace WalkInDrive.Models;

public partial class Technology
{
    public int TechnologyId { get; set; }

    public string? TechnologyName { get; set; }

    public string? OtherTechnology { get; set; }

    public DateTime DtCreated { get; set; }

    public DateTime DtModified { get; set; }

    public virtual ICollection<ProfessionalDetail>? ProfessionalDetailsWorks { get; set; } = new List<ProfessionalDetail>();

    public virtual ICollection<ProfessionalDetail>? ProfessionalDetailsWorksNavigation { get; set; } = new List<ProfessionalDetail>();
}
