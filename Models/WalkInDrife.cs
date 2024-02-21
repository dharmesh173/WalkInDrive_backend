using System;
using System.Collections.Generic;

namespace WalkInDrive.Models;

public partial class WalkInDrife
{
    public int DriveId { get; set; }

    public string DriveTitle { get; set; } = null!;

    public DateOnly DriveStartDate { get; set; }

    public DateOnly DriveEndDate { get; set; }

    public string Location { get; set; } = null!;

    public string OtherDetails { get; set; } = null!;

    public TimeOnly Time { get; set; }

    public DateTime DtCreated { get; set; }

    public DateTime DtModified { get; set; }

    public virtual ICollection<DriveAvailableSlot>? DriveAvailableSlots { get; set; } = new List<DriveAvailableSlot>();

    public virtual ICollection<Role>? Roles { get; set; } = new List<Role>();
}
