using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biashara_POS.Models
{
    public class PurchaseItem
    {
        [Key]
        public int PurchaseItemId { get; set; }

        // --------------------
        // FOREIGN KEYS
        // --------------------
        [Required]
        public int PurchaseId { get; set; }

        [Required]
        public int ProductId { get; set; }

        // --------------------
        // QUANTITY & PRICE
        // --------------------
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        [NotMapped]
        public decimal Total => Quantity * UnitPrice;

        // --------------------
        // NAVIGATION
        // --------------------
        public Purchase Purchase { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
