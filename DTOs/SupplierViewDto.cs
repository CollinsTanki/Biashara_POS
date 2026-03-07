using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class SupplierViewDto
    {
        public int SupplierId { get; set; }

        public string SupplierName { get; set; } = "";

        public string PhoneNumber { get; set; } = "";

        public string Email { get; set; } = "";

        public string Location { get; set; } = "";

        public string Address { get; set; } = "";

        public int PurchaseCount { get; set; }

        public decimal Balance { get; set; }
    }
}