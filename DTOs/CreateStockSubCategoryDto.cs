using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs.StockSubCategory
{
    public class CreateStockSubCategoryDto
    {
        [Required(ErrorMessage = "Category is required")]
        public int StockCategoryId { get; set; }

        [Required(ErrorMessage = "Sub category name is required")]
        [MaxLength(100, ErrorMessage = "Maximum length is 100 characters")]
        public string SubCategoryName { get; set; } = string.Empty;
    }
}