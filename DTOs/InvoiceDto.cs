namespace Biashara_POS.DTOs
{
    public class InvoiceDto
    {
        public int InvoiceId { get; set; }

        public string InvoiceNumber { get; set; } = string.Empty;

        public DateTime InvoiceDate { get; set; } = DateTime.Now;

        public int CustomerId { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public decimal SubTotal { get; set; }

        public decimal VatTotal { get; set; }

        public decimal DiscountTotal { get; set; }

        public decimal GrandTotal { get; set; }

        public bool IsPaid { get; set; }

        public List<InvoiceItemDto> Items { get; set; } = new();
    }
}