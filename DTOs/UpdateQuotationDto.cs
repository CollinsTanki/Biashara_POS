using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class UpdateQuotationDto
    {
        public int QuotationId { get; set; }

        [Required]
        public DateTime ValidUntil { get; set; }

        public bool IsConfirmed { get; set; }
    }
}