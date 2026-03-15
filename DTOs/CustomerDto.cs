namespace Biashara_POS.DTOs
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }

        public string FullName { get; set; } = "";

        public string PhoneNumber { get; set; } = "";

        public string Location { get; set; } = "";

        public decimal LoyaltyPoints { get; set; }

        public decimal BalanceBroughtForward { get; set; }

        public decimal CreditLimit { get; set; }

        public bool IsWalkIn { get; set; }

        public bool IsActive { get; set; }
    }
}