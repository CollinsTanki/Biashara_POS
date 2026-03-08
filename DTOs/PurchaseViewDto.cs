namespace Biashara_POS.DTOs
{
    public class PurchaseViewDto
    {
        public int PurchaseId { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string SupplierName { get; set; }

        public decimal TotalAmount { get; set; }

        public bool IsCredit { get; set; }

        public int ItemCount { get; set; }
    }
}