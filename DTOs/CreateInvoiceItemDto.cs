using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs.Invoices
{
    public class CreateInvoiceDto
    {
        [Required]
        public int CustomerId { get; set; }

        public DateTime InvoiceDate { get; set; } = DateTime.Now;

        public List<InvoiceItemDto> Items { get; set; } = new();
    }
}