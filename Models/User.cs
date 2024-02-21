using System;
using System.Collections.Generic;

namespace WalkInDrive.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public byte[] ProfilePic { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public sbyte GetUpdate { get; set; }

    public byte[] Resume { get; set; } = null!;

    public string? PortfolioUrl { get; set; }

    public string ReferredPersonName { get; set; } = null!;

    public DateTime DtCreated { get; set; }

    public DateTime DtModified { get; set; }

    public virtual ICollection<DriveApplied>? DriveApplieds { get; set; } = new List<DriveApplied>();

    public virtual ICollection<EducationDetail>? EducationDetails { get; set; } = new List<EducationDetail>();

    public virtual ICollection<ProfessionalDetail>? ProfessionalDetails { get; set; } = new List<ProfessionalDetail>();

    public virtual ICollection<Role>? Roles { get; set; } = new List<Role>();
}
