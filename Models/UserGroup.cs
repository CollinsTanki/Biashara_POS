using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.Models
{
    public class UserGroup
    {
        [Key]
        public int UserGroupId { get; set; }

        // REQUIRED + LENGTH LIMIT
        [Required]
        [MaxLength(50)]
        public string GroupName { get; set; } = "";  // Cashier, Admin

        [MaxLength(200)]
        public string Description { get; set; } = "";

        public bool IsEditable { get; set; } = true;

        // NAVIGATION PROPERTIES
        public ICollection<AppUser> Users { get; set; } = new List<AppUser>();

        public ICollection<GroupPermission> GroupPermissions { get; set; }
            = new List<GroupPermission>();

    }
}



