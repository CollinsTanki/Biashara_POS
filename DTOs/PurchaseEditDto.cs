using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class PurchaseEditDto
    {
        public int PurchaseId { get; set; }

        [Required]
        public int SupplierId { get; set; }

        public DateTime PurchaseDate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        public bool IsCredit { get; set; }
    }
}