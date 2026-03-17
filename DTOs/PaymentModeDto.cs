using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class PaymentModeDto
    {
        public int PaymentModeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ModeName { get; set; } = "";

        public bool IsActive { get; set; } = true;
    }
}