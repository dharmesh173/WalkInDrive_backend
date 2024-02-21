using System;
using System.Collections.Generic;

namespace WalkInDrive.Models;

public partial class ProfessionalDetail
{
    public int Id { get; set; }

    public string ApplicantType { get; set; } = null!;

    public int Experience { get; set; }

    public int? CurrentCtc { get; set; }

    public int ExpectedCtc { get; set; }

    public sbyte? NoticePeriod { get; set; }

    public sbyte PreAppear { get; set; }

    public string? PreappearRole { get; set; }

    public DateOnly? NoticePeriodEnd { get; set; }

    public string? MonthOfNp { get; set; }

    public DateTime DtCreated { get; set; }

    public DateTime? DtModified { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; } = null!;

    public virtual ICollection<Technology>? Technologies { get; set; } = new List<Technology>();

    public virtual ICollection<Technology>? TechnologiesNavigation { get; set; } = new List<Technology>();
}
