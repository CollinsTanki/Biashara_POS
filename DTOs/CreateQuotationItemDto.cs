using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class CreateQuotationItemDto
    {
        [Required]
        public int QuotationId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 100000)]
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public decimal VatAmount { get; set; }
    }
}