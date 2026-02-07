using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.Models
{
    public class UserGroup
    {
        public int UserGroupId { get; set; }

        [Required]
        public string GroupName { get; set; } = "";  // Cashier, Admin

        public string Description { get; set; } = "";

        public bool IsEditable { get; set; } = true;

        public ICollection<User> Users { get; set; } 
        public ICollection<GroupPermission> GroupPermissions { get; set; } 
    }

}

