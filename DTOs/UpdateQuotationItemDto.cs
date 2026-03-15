using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class UpdateQuotationItemDto
    {
        public int QuotationItemId { get; set; }

        [Range(1, 100000)]
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public decimal VatAmount { get; set; }
    }
}