using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class PurchaseCreateDto
    {
        [Required]
        public int SupplierId { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.Now;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        public bool IsCredit { get; set; }
    }
}