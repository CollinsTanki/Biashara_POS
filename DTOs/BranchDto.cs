using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class BranchDto
    {
        public int BranchId { get; set; }

        [Required(ErrorMessage = "Company is required")]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Branch name is required")]
        [MaxLength(150)]
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Address { get; set; }  // nullable, optional

        [MaxLength(20)]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string? Phone { get; set; }  // nullable, optional

        // Robust email validation
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(150)]
        [RegularExpression(
            @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            ErrorMessage = "Please enter a valid email address"
        )]
        public string Email { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Location { get; set; }  // nullable, optional

        [Display(Name = "Head Office")]
        public bool IsHQ { get; set; }
    }
}