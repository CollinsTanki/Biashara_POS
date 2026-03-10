using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class CreateSaleDto
    {
        public int? CustomerId { get; set; }

        public bool IsCreditSale { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }
    }
}