using System;
using System.Collections.Generic;

namespace WalkInDrive.Models;

public partial class DriveAvailableSlot
{
    public int SlotsId { get; set; }

    public int WalkInDrivesDriveId { get; set; }

    public virtual ICollection<DriveApplied> DriveApplieds { get; set; } = new List<DriveApplied>();

    public virtual Slot Slots { get; set; } = null!;

    public virtual WalkInDrife? WalkInDrivesDrive { get; set; } = null!;
}
