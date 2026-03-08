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
        [Range(0.01, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        [Column(TypeName = "decimal(18,2)")]  // Ensures database stores decimals
        public decimal Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Unit price cannot be negative")]
        public decimal UnitPrice { get; set; }

        [NotMapped]
        public decimal Total => Quantity * UnitPrice;

        // --------------------
        // NAVIGATION PROPERTIES
        // --------------------
        public Purchase Purchase { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}