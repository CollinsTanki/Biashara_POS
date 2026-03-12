using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs.Users
{
    public class UserCreateDto
    {
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
        public int? BranchId { get; set; } // nullable now

        [Display(Name = "User Group")]
        public int? UserGroupId { get; set; } // nullable

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;
    }
}