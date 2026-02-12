using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.Models
{
    public class PaymentMode
    {
        [Key]
        public int PaymentModeId { get; set; }

        // REQUIRED + LIMIT LENGTH
        [Required]
        [MaxLength(50)]
        public string ModeName { get; set; } = ""; // Cash, Paybill, M-Pesa, Bank

        public bool IsActive { get; set; } = true;
    }
}
