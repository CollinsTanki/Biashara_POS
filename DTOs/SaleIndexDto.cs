namespace Biashara_POS.DTOs
{
    public class SaleIndexDto
    {
        public int SaleId { get; set; }

        public string ReceiptNumber { get; set; } = "";

        public DateTime SaleDate { get; set; }

        public string CustomerName { get; set; } = "Walk-in";

        public decimal TotalAmount { get; set; }

        public decimal Balance { get; set; }

        public bool IsCreditSale { get; set; }

        public string Cashier { get; set; } = "";
    }
}