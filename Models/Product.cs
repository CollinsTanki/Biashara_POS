using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biashara_POS.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        // --------------------
        // PRODUCT DETAILS
        // --------------------
        [Required]
        [MaxLength(150)]
        public string ProductName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Barcode { get; set; }

        public bool IsActive { get; set; } = true;

        // --------------------
        // FOREIGN KEYS
        // --------------------
        [Required]
        public int StockCategoryId { get; set; }

        [Required]
        public int StockSubCategoryId { get; set; }

        [Required]
        public int StockMeasureId { get; set; }

        [Required]
        public int VatSetupId { get; set; }

        // --------------------
        // PRICING
        // --------------------
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal BuyingPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal SellingPrice { get; set; }

        [Range(0, int.MaxValue)]
        public int ReorderLevel { get; set; }

        // --------------------
        // NAVIGATION
        // --------------------
        public StockCategory StockCategory { get; set; } = null!;
        public StockSubCategory StockSubCategory { get; set; } = null!;
        public StockMeasure StockMeasure { get; set; } = null!;
        public VatSetup VatSetup { get; set; } = null!;
    }
}
