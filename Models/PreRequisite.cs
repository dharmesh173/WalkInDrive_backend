using System;
using System.Collections.Generic;

namespace WalkInDrive.Models;

public partial class PreRequisite
{
    public int Id { get; set; }

    public string? GeneralInst { get; set; }

    public string? ExamInst { get; set; }

    public string? SystemRequirement { get; set; }

    public string? Process { get; set; }

    public DateOnly? Round1Date { get; set; }

    public DateOnly? Round2Date { get; set; }

    public string? Round1Type { get; set; }

    public string? Round2Type { get; set; }

    public DateTime? DtCreated { get; set; }

    public DateTime? DtModified { get; set; }
}
