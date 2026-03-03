namespace Biashara_POS.DTOs.StockSubCategory
{
    public class StockSubCategoryViewDto
    {
        public int StockSubCategoryId { get; set; }

        public int StockCategoryId { get; set; }

        public string SubCategoryName { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        // Instead of full navigation object
        public string CategoryName { get; set; } = string.Empty;
    }
}