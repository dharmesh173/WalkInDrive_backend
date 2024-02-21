using System;
using System.Collections.Generic;

namespace WalkInDrive.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string RoleDescription { get; set; } = null!;

    public string RoleRequirements { get; set; } = null!;

    public int GrossCompensationPkg { get; set; }

    public DateTime DtCreated { get; set; }

    public DateTime? DtModified { get; set; }

    public virtual ICollection<DriveApplied>? DriveApplieds { get; set; } = new List<DriveApplied>();

    public virtual ICollection<User>? Users { get; set; } = new List<User>();

    public virtual ICollection<WalkInDrife>? WalkinDrives { get; set; } = new List<WalkInDrife>();
}
