namespace Biashara_POS.Models
{
    public class Quotation
    {
        public int QuotationId { get; set; }

        public string RefNumber { get; set; } = "";

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime ValidUntil { get; set; }

        public int CustomerId { get; set; }

        public decimal TotalAmount { get; set; }

        public bool IsConfirmed { get; set; }

        public Customer Customer { get; set; } 

        public ICollection<QuotationItem> QuotationItems { get; set; }
    }
}
