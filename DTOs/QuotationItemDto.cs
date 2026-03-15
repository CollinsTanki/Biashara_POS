namespace Biashara_POS.DTOs
{
    public class QuotationItemDto
    {
        public int QuotationItemId { get; set; }

        public int QuotationId { get; set; }

        public string ProductName { get; set; } = "";

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public decimal VatAmount { get; set; }

        public decimal SubTotal { get; set; }
    }
}