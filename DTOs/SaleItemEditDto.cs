using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class SaleItemEditDto
    {
        public int SaleItemId { get; set; }

        public int SaleId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public decimal VatAmount { get; set; }
    }
}