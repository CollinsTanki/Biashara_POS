using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }

        [Required, MaxLength(150)]
        public string SupplierName { get; set; } = "";

        public string Address { get; set; } = "";

        [MaxLength(20)]
        public string PhoneNumber { get; set; } = "";

        public string Email { get; set; } = "";

        public string Location { get; set; } = "";

        public ICollection<Purchase> Purchases { get; set; }
    }
}
