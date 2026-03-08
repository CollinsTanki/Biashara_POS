using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class PurchaseItemCreateDto
    {
        [Required]
        public int PurchaseId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
    }
}