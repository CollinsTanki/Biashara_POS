using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biashara_POS.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        // --------------------
        // ITEM DETAILS
        // --------------------
        [Required]
        [MaxLength(150)]
        public string ItemName { get; set; } = string.Empty;

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

        public bool IsActive { get; set; } = true;

        // --------------------
        // NAVIGATION
        // --------------------
        [ForeignKey(nameof(StockCategoryId))]
        public StockCategory StockCategory { get; set; } = null!;

        [ForeignKey(nameof(StockSubCategoryId))]
        public StockSubCategory StockSubCategory { get; set; } = null!;

        [ForeignKey(nameof(StockMeasureId))]
        public StockMeasure StockMeasure { get; set; } = null!;

        [ForeignKey(nameof(VatSetupId))]
        public VatSetup VatSetup { get; set; } = null!;

    }
}
