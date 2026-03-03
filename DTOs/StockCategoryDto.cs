using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class StockCategoryDto
    {
        public int StockCategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [MaxLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; } = string.Empty;

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        // Navigation DTO (Optional)
        public ICollection<StockSubCategoryDto> SubCategories { get; set; }
            = new List<StockSubCategoryDto>();
    }
}