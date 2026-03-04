using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class UpdateStockMeasureDto
    {
        [Required]
        public int StockMeasureId { get; set; }


        [Required(ErrorMessage = "Measure name is required.")]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "Measure name must be between 2 and 50 characters.")]
        [Display(Name = "Measure Name")]
        public string MeasureName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Initials are required.")]
        [StringLength(10, MinimumLength = 1,
            ErrorMessage = "Initials must be between 1 and 10 characters.")]
        [Display(Name = "Initials")]
        [RegularExpression("^[a-zA-Z]+$",
            ErrorMessage = "Initials must contain letters only.")]
        public string Initials { get; set; } = string.Empty;
    }
}