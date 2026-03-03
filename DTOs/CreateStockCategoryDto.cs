using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class CreateStockCategoryDto
    {
        [Required(ErrorMessage = "Category name is required")]
        [MaxLength(100)]
        public string CategoryName { get; set; } = string.Empty;
    }
}
