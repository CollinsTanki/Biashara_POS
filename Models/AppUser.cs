using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biashara_POS.Models
{
    public class AppUser : IdentityUser
    {
        // ---------------- USER PROFILE ----------------

        [Required]
        [MaxLength(150)]
        public string FullName { get; set; } = "";

        [MaxLength(100)]
        public string? Position { get; set; }   // e.g Cashier, Manager

        [MaxLength(250)]
        public string? Address { get; set; }

        public string? ProfilePhoto { get; set; }


        // ---------------- STATUS ----------------

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? LastLoginDate { get; set; }


        // ---------------- FOREIGN KEYS ----------------

        [Required]
        public int UserGroupId { get; set; }

        [Required]
        public int BranchId { get; set; }


        // ---------------- NAVIGATION ----------------

        [ForeignKey(nameof(UserGroupId))]
        public UserGroup? UserGroup { get; set; }

        [ForeignKey(nameof(BranchId))]
        public Branch? Branch { get; set; }


        // ---------------- SALES RELATION ----------------

        public ICollection<Sale>? Sales { get; set; }
    }
}