using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Biashara_POS.DTOs
{
    public class ProductDto
    {
        // =============================
        // PRIMARY KEY
        // =============================
        public int ProductId { get; set; }


        // =============================
        // PRODUCT INFORMATION
        // =============================

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(150)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Barcode")]
        public string? Barcode { get; set; }

        [Display(Name = "Active Product")]
        public bool IsActive { get; set; } = true;


        // =============================
        // PRODUCT CLASSIFICATION
        // =============================

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int StockCategoryId { get; set; }

        [Required(ErrorMessage = "Sub category is required")]
        [Display(Name = "Sub Category")]
        public int StockSubCategoryId { get; set; }

        [Required(ErrorMessage = "Unit of measure is required")]
        [Display(Name = "Unit Measure")]
        public int StockMeasureId { get; set; }

        [Required(ErrorMessage = "VAT setup is required")]
        [Display(Name = "VAT Setup")]
        public int VatSetupId { get; set; }


        // =============================
        // PRICING
        // =============================

        [Range(0, double.MaxValue, ErrorMessage = "Buying price must be positive")]
        [Display(Name = "Buying Price")]
        public decimal BuyingPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Selling price must be positive")]
        [Display(Name = "Selling Price")]
        public decimal SellingPrice { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Reorder level must be positive")]
        [Display(Name = "Reorder Level")]
        public int ReorderLevel { get; set; }


        // =============================
        // PRODUCT IMAGE
        // =============================

        // Stored in database
        [Display(Name = "Product Image")]
        public string? ImagePath { get; set; }

        // Uploaded from form (not stored directly)
        [Required(ErrorMessage = "Product image is required")]
        [Display(Name = "Upload Image")]
        public IFormFile? ImageFile { get; set; }


        // =============================
        // DISPLAY FIELDS (Index View)
        // =============================

        public string? CategoryName { get; set; }

        public string? SubCategoryName { get; set; }

        public string? MeasureName { get; set; }

        public string? VatName { get; set; }


        // =============================
        // COMPUTED FIELD
        // =============================

        [Display(Name = "Profit")]
        public decimal Profit => SellingPrice - BuyingPrice;
    }
}