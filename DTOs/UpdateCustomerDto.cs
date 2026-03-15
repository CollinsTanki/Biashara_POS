using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class UpdateCustomerDto
    {
        public int CustomerId { get; set; }

        [Required, MaxLength(150)]
        public string FullName { get; set; } = "";

        [MaxLength(20)]
        public string PhoneNumber { get; set; } = "";

        public string Location { get; set; } = "";

        public decimal CreditLimit { get; set; }

        public bool IsActive { get; set; }
    }
}