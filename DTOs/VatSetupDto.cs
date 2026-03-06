using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class VatSetupDto
    {
        public int VatSetupId { get; set; }

        [Required]
        [MaxLength(50)]
        public string VatName { get; set; } = "";

        [Required]
        [MaxLength(10)]
        public string VatInitials { get; set; } = "";

        [Range(0, 100)]
        public decimal TaxRate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}