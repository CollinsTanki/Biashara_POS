using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biashara_POS.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public int SaleId { get; set; }

        [Required]
        public int PaymentModeId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [MaxLength(100)]
        public string? ReferenceNumber { get; set; }

        public string? ReceivedByUserId { get; set; }

        // Navigation (nullable to avoid warnings)
        [ForeignKey(nameof(SaleId))]
        public Sale? Sale { get; set; }

        [ForeignKey(nameof(PaymentModeId))]
        public PaymentMode? PaymentMode { get; set; }

        [ForeignKey(nameof(ReceivedByUserId))]
        public AppUser? ReceivedByUser { get; set; }
    }
}