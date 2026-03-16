namespace Biashara_POS.DTOs
{
    public class InvoiceItemDto
    {
        public int InvoiceItemId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = "";

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Total { get; set; }
    }
}
