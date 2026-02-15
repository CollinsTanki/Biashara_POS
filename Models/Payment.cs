using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        public int SaleId { get; set; }

        [Required]
        public int PaymentModeId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public string? ReferenceNumber { get; set; }

        public string? ReceivedByUserId { get; set; }

        // Navigation
        public Sale Sale { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public AppUser? ReceivedByUser { get; set; }
    }

}
