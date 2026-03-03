using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class StockSubCategoryDto
    {
        public int StockSubCategoryId { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int StockCategoryId { get; set; }

        [Required(ErrorMessage = "Sub category name is required")]
        [MaxLength(100, ErrorMessage = "Maximum length is 100 characters")]
        [Display(Name = "Sub Category Name")]
        public string SubCategoryName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        // ✅ Navigation DTO (NOT entity)
        public StockCategoryDto? StockCategory { get; set; }
    }
}