namespace Biashara_POS.DTOs
{
    public class PaymentDto
    {
        public int PaymentId { get; set; }

        public int SaleId { get; set; }

        public int PaymentModeId { get; set; }

        public string PaymentModeName { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string? ReferenceNumber { get; set; }
    }
}