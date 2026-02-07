using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; } = "";

        [Required]
        public string Email { get; set; } = "";

        public string PasswordHash { get; set; } = "";

        public bool IsActive { get; set; } = true;

        // FK
        public int UserGroupId { get; set; }
        public int BranchId { get; set; }

        // Navigation
        public UserGroup UserGroup { get; set; } 
        public Branch Branch { get; set; }
    }
}
