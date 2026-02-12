using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.Models
{
    public class VatSetup
    {
        [Key]
        public int VatSetupId { get; set; }

        // REQUIRED + LENGTH LIMITS
        [Required]
        [MaxLength(50)]
        public string VatName { get; set; } = "";   // VAT 16%

        [Required]
        [MaxLength(10)]
        public string VatInitials { get; set; } = ""; // VAT, ZERO

        // TAX RATE (e.g. 16.00)
        [Range(0, 100)]
        public decimal TaxRate { get; set; }
    }
}
