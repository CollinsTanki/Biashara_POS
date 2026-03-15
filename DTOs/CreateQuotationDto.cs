using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class CreateQuotationDto
    {
        [Required]
        public string RefNumber { get; set; } = "";

        [Required]
        public DateTime ValidUntil { get; set; }

        [Required]
        public int CustomerId { get; set; }
    }
}