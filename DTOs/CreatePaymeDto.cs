using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class CreatePaymentDto
    {
        [Required]
        public int SaleId { get; set; }

        [Required]
        public int PaymentModeId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string? ReferenceNumber { get; set; }
    }
}