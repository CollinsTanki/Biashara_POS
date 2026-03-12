using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs.Users
{
    public class UserEditDto
    {
        [Required]
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "Full name is required")]
        [MaxLength(150)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Position { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

        [Display(Name = "Branch")]
        public int? BranchId { get; set; } // nullable

        [Display(Name = "User Group")]
        public int? UserGroupId { get; set; } // nullable

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}