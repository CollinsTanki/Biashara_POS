using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biashara_POS.Models
{
    public class StockSubCategory
    {
        [Key]
        public int StockSubCategoryId { get; set; }

        // FOREIGN KEY
        [Required]
        [ForeignKey(nameof(StockCategory))]
        public int StockCategoryId { get; set; }

        // REQUIRED + LENGTH LIMIT
        [Required]
        [MaxLength(100)]
        public string SubCategoryName { get; set; } = "";

        public bool IsActive { get; set; } = true;

        // NAVIGATION PROPERTY
        public StockCategory StockCategory { get; set; } = null!;

    }
}
