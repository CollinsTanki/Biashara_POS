namespace Biashara_POS.DTOs
{
    public class QuotationDto
    {
        public int QuotationId { get; set; }

        public string RefNumber { get; set; } = "";

        public DateTime CreatedDate { get; set; }

        public DateTime ValidUntil { get; set; }

        public string CustomerName { get; set; } = "";

        public decimal TotalAmount { get; set; }

        public bool IsConfirmed { get; set; }
    }
}