using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biashara_POS.Models
{
    public class AppUser : IdentityUser
    {
        public int UserId { get; set; }

        [Required]
        [Key]
        public int AppUserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        // ---------- Foreign Keys ----------  
        [Required]
        public int UserGroupId { get; set; }

        [Required]
        public int BranchId { get; set; }

        // ---------- Navigation ----------  
        [ForeignKey(nameof(UserGroupId))]
        public UserGroup UserGroup { get; set; }

        [ForeignKey(nameof(BranchId))]
        public Branch Branch { get; set; }
    }
}
