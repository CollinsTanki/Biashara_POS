using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biashara_POS.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required, MaxLength(150)]
        public string ProductName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Barcode { get; set; }

        public bool IsActive { get; set; } = true;

        // --------------------
        // STOCK INFORMATION
        // --------------------
        [Column(TypeName = "decimal(18,2)")]
        public decimal StockQuantity { get; set; } = 0;

        public int ReorderLevel { get; set; }

        // --------------------
        // FOREIGN KEYS
        // --------------------
        public int StockCategoryId { get; set; }
        public int StockSubCategoryId { get; set; }
        public int StockMeasureId { get; set; }
        public int VatSetupId { get; set; }

        // --------------------
        // PRICING
        // --------------------
        [Column(TypeName = "decimal(18,2)")]
        public decimal BuyingPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SellingPrice { get; set; }

        // --------------------
        // IMAGE
        // --------------------
        [MaxLength(250)]
        [Display(Name = "Product Image")]
        public string? ImagePath { get; set; } // stored in wwwroot/images/products

        // --------------------
        // NAVIGATION
        // --------------------
        public StockCategory StockCategory { get; set; } = null!;
        public StockSubCategory StockSubCategory { get; set; } = null!;
        public StockMeasure StockMeasure { get; set; } = null!;
        public VatSetup VatSetup { get; set; } = null!;
    }
}