using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.Models
{
    public class QuotationItem
    {
        [Key]
        public int QuotationItemId { get; set; }

        // --------------------
        // FOREIGN KEYS
        // --------------------
        [Required]
        public int QuotationId { get; set; }

        [Required]
        public int ProductId { get; set; }

        // --------------------
        // ITEM DETAILS
        // --------------------
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Discount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal VatAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }

        // --------------------
        // NAVIGATION
        // --------------------
        [ForeignKey(nameof(QuotationId))]
        public Quotation Quotation { get; set; } = null!;

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

    }
}
