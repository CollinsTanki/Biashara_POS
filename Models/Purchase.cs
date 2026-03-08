using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.Models
{
    public class Purchase
    {
        [Key]
        public int PurchaseId { get; set; }

        // -----------------------------
        // PURCHASE NUMBER (For receipts & tracking)
        // -----------------------------
        [Required]
        [StringLength(20)]
        public string PurchaseNumber { get; set; } = "";

        // -----------------------------
        // PURCHASE DATE
        // -----------------------------
        [Required]
        public DateTime PurchaseDate { get; set; } = DateTime.Now;

        // -----------------------------
        // SUPPLIER
        // -----------------------------
        [Required]
        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; } = null!;

        // -----------------------------
        // TOTAL PURCHASE AMOUNT
        // -----------------------------
        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        // -----------------------------
        // CREDIT PURCHASE FLAG
        // -----------------------------
        public bool IsCredit { get; set; } = false;

        // -----------------------------
        // PURCHASE ITEMS
        // -----------------------------
        public ICollection<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();
    }
}