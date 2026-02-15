using System.ComponentModel.DataAnnotations;

namespace Biashara_POS.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required, MaxLength(150)]
        public string FullName { get; set; } = "";

        [MaxLength(20)]
        public int PhoneNumber { get; set; }

        [MaxLength(200)]
        public string Location { get; set; } = "";

        public decimal LoyaltyPoints { get; set; } = 0;

        public decimal BalanceBroughtForward { get; set; } = 0;

        public decimal CreditLimit { get; set; } = 0;

        public bool IsWalkIn { get; set; } = false;

        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<Sale> Sales { get; set; }

    }
}
