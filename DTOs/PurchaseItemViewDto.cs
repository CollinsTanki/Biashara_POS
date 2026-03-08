using System;

namespace Biashara_POS.DTOs
{
    public class PurchaseItemViewDto
    {
        public int PurchaseItemId { get; set; }

        public int PurchaseId { get; set; }

        public string ProductName { get; set; } = "";

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }
    }
}