using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class SupplierEditDto
    {
        public int SupplierId { get; set; }

        [Required]
        [MaxLength(150)]
        public string SupplierName { get; set; } = "";

        public string Address { get; set; } = "";

        [MaxLength(20)]
        public string PhoneNumber { get; set; } = "";

        [EmailAddress]
        public string Email { get; set; } = "";

        public string Location { get; set; } = "";
    }
}