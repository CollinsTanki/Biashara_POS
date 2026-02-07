using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.Models
{
    public class Company
    {
        public int CompanyId { get; set; }

        [Required]
        public string CompanyName { get; set; } = "";

        [Required]
        public string RegistrationType { get; set; } = ""; // Individual / Business

        public string KRAPin { get; set; } = "";

        [Required]
        public string Email { get; set; } = "";

        [Required]
        public string Phone { get; set; } = "";

        public string LogoPath { get; set; } = "";

        public ICollection<Branch> Branches { get; set; }
    }
}
