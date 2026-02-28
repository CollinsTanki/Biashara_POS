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

        // Foreign keys
        public int StockCategoryId { get; set; }
        public int StockSubCategoryId { get; set; }
        public int StockMeasureId { get; set; }
        public int VatSetupId { get; set; }

        // Pricing
        [Column(TypeName = "decimal(18,2)")]
        public decimal BuyingPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SellingPrice { get; set; }

        public int ReorderLevel { get; set; }

        // --------------------
        // IMAGE
        // --------------------
        [MaxLength(250)]
        [Display(Name = "Product Image")]
        public string? ImagePath { get; set; } // path in wwwroot/images/products

        // Navigation
        public StockCategory StockCategory { get; set; } = null!;
        public StockSubCategory StockSubCategory { get; set; } = null!;
        public StockMeasure StockMeasure { get; set; } = null!;
        public VatSetup VatSetup { get; set; } = null!;
    }
}