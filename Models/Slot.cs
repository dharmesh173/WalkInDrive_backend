using System;
using System.Collections.Generic;

namespace WalkInDrive.Models;

public partial class Slot
{
    public int Id { get; set; }

    public string? SlotTiming { get; set; }

    public DateTime DtCreated { get; set; }

    public DateTime DtModified { get; set; }

    public virtual ICollection<DriveAvailableSlot>? DriveAvailableSlots { get; set; } = new List<DriveAvailableSlot>();
}
