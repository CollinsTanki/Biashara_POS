using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        // --------------------
        // COMPANY DETAILS
        // --------------------
        [Required]
        [MaxLength(150)]
        public string CompanyName { get; set; } = string.Empty;

        // Individual | Business
        [Required]
        [MaxLength(50)]
        public string RegistrationType { get; set; } = string.Empty;

        // KRA PIN (required only for Business – enforced in DB)
        [MaxLength(20)]
        public string KRAPin { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(250)]
        public string LogoPath { get; set; } = string.Empty;

        // --------------------
        // NAVIGATION
        // --------------------
        public ICollection<Branch> Branches { get; set; } = new List<Branch>();



    }
}
