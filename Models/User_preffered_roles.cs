using System.ComponentModel.DataAnnotations;

namespace WalkInDrive.Models
{
    public class User_preffered_roles
    {
        [Key]
        public int user_id { get; set; }

        public int role_id { get; set; }
    }
}
