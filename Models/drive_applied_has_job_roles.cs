using System.ComponentModel.DataAnnotations;

namespace WalkInDrive.Models
{
    public class drive_applied_has_job_roles
    {
        [Key]
        public int drive_applied_id {  get; set; }

        public int roles_role_id { get; set; }
    }
}
