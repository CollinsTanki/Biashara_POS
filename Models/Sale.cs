using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biashara_POS.Models
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ReceiptNumber { get; set; } = Guid.NewGuid().ToString();

        public DateTime SaleDate { get; set; } = DateTime.Now;

        // Walk-ins allowed
        public int? CustomerId { get; set; }

        public bool IsCreditSale { get; set; } = false;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        // Remaining unpaid amount
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        [Required]
        public string UserId { get; set; } = "";  // Cashier

        // Navigation
        public Customer? Customer { get; set; }
        public AppUser? User { get; set; }

        public ICollection<SaleItem>? SaleItems { get; set; }
        public ICollection<Payment>? Payments { get; set; }

    }
}
