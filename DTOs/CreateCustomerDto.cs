using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.DTOs
{
    public class CreateCustomerDto
    {
        [Required, MaxLength(150)]
        public string FullName { get; set; } = "";

        [MaxLength(20)]
        [RegularExpression(@"^[0-9+]*$", ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; } = "";

        [MaxLength(200)]
        public string Location { get; set; } = "";

        public decimal CreditLimit { get; set; }

        public bool IsWalkIn { get; set; }
    }
}