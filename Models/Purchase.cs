namespace Biashara_POS.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.Now;

        public int SupplierId { get; set; }

        public decimal TotalAmount { get; set; }

        public bool IsCredit { get; set; }

        public Supplier Supplier { get; set; } 

        public ICollection<PurchaseItem> PurchaseItems { get; set; }
    }
}
