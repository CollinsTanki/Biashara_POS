using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Biashara_POS.Models
{
    public class CompanyDto
    {
        [Required(ErrorMessage = "Company name is required.")]
        [MaxLength(150)]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Registration type is required.")]
        [MaxLength(50)]
        public string RegistrationType { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? KRAPin { get; set; }   // Optional field

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Company logo is required.")]
        public IFormFile? ImageFile { get; set; }
    }
}