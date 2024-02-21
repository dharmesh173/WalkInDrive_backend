using System;
using System.Collections.Generic;

namespace WalkInDrive.Models;

public partial class EducationDetail
{
    public string CollegeName { get; set; } = null!;

    public int Id { get; set; }

    public string Location { get; set; } = null!;

    public float Percentage { get; set; }

    public int PassoutYear { get; set; }

    public string Qualification { get; set; } = null!;

    public string Stream { get; set; } = null!;

    public DateTime DtCreated { get; set; }

    public DateTime DtModified { get; set; }

    public int UserId { get; set; }

    public virtual User? User { get; set; } = null!;
}
