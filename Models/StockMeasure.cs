using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.Models
{
    public class StockMeasure
    {
        [Key]
        public int StockMeasureId { get; set; }

        // REQUIRED + LENGTH LIMIT
        [Required]
        [MaxLength(50)]
        public string MeasureName { get; set; } = ""; // Kilograms

        [Required]
        [MaxLength(10)]
        public string Initials { get; set; } = ""; // Kgs

    }
}
