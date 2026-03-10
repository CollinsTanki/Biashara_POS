using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class SaleItemCreateDto
    {
        [Required]
        public int SaleId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public decimal VatAmount { get; set; }
    }
}