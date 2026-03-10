using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class SaleEditDto
    {
        public int SaleId { get; set; }

        public int? CustomerId { get; set; }

        public bool IsCreditSale { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public decimal Balance { get; set; }
    }
}