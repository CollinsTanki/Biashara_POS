using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.Models
{
    public class StockCategory
    {
        [Key]
        public int StockCategoryId { get; set; }

        // REQUIRED + LENGTH LIMIT
        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; } = "";

        public bool IsActive { get; set; } = true;

        // NAVIGATION (One category → many sub-categories)
        public ICollection<StockSubCategory> SubCategories { get; set; } = new List<StockSubCategory>();

    }
}
