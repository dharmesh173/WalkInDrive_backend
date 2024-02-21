using System;
using System.Collections.Generic;

namespace WalkInDrive.Models;

public partial class DriveApplied
{
    public int Id { get; set; }

    public byte[]? Resume { get; set; }

    public DateTime DtCreated { get; set; }

    public DateTime DtModified { get; set; }

    public int SlotId { get; set; }

    public int DriveId { get; set; }

    public int UserId { get; set; }

    public virtual DriveAvailableSlot? DriveAvailableSlot { get; set; } = null!;

    public virtual User? User { get; set; } = null!;

    public virtual ICollection<Role>? RolesRoles { get; set; } = new List<Role>();
}
