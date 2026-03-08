using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class PurchaseItemEditDto
    {
        public int PurchaseItemId { get; set; }

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