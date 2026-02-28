using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http; // required for IFormFile

namespace Biashara_POS.DTOs
{
    public class ProductDto
    {
        // --------------------
        // PRIMARY KEY
        // --------------------
        public int ProductId { get; set; }

        // --------------------
        // PRODUCT DETAILS
        // --------------------
        [Required(ErrorMessage = "Product name is required")]
        [MaxLength(150)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = string.Empty;

        [MaxLength(100)]
        [Display(Name = "Barcode")]
        public string? Barcode { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;

        // --------------------
        // FOREIGN KEYS
        // --------------------
        [Required]
        [Display(Name = "Category")]
        public int StockCategoryId { get; set; }

        [Required]
        [Display(Name = "Sub Category")]
        public int StockSubCategoryId { get; set; }

        [Required]
        [Display(Name = "Unit Measure")]
        public int StockMeasureId { get; set; }

        [Required]
        [Display(Name = "VAT Setup")]
        public int VatSetupId { get; set; }

        // --------------------
        // PRICING
        // --------------------
        [Range(0, double.MaxValue)]
        [Display(Name = "Buying Price")]
        public decimal BuyingPrice { get; set; }

        [Range(0, double.MaxValue)]
        [Display(Name = "Selling Price")]
        public decimal SellingPrice { get; set; }

        [Range(0, int.MaxValue)]
        [Display(Name = "Reorder Level")]
        public int ReorderLevel { get; set; }

        // --------------------
        // IMAGE PROPERTIES
        // --------------------

        // Path stored in DB
        [Display(Name = "Product Image")]
        public string? ImagePath { get; set; }

        // File uploaded via form (not mapped to DB)
        [Display(Name = "Upload Image")]
        public IFormFile? ImageFile { get; set; }

        // --------------------
        // DISPLAY FIELDS (For Index)
        // --------------------
        public string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }
        public string? MeasureName { get; set; }
        public string? VatName { get; set; }

        // --------------------
        // COMPUTED PROPERTY
        // --------------------
        public decimal Profit => SellingPrice - BuyingPrice;
    }
}