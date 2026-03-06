namespace Biashara_POS.DTOs
{
    public class VatSetupViewDto
    {
        public int VatSetupId { get; set; }
        public string VatName { get; set; } = "";
        public string VatInitials { get; set; } = "";
        public decimal TaxRate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}