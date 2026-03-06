using Biashara_POS.Data;
using Biashara_POS.DTOs;
using Biashara_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Biashara_POS.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;

        public ProductController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }

        // ============================
        // INDEX
        // ============================
        public async Task<IActionResult> Index()
        {
            var productDtos = await context.Products
                .Include(p => p.StockCategory)
                .Include(p => p.StockSubCategory)
                .Include(p => p.StockMeasure)
                .Include(p => p.VatSetup)
                .Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Barcode = p.Barcode,
                    IsActive = p.IsActive,
                    BuyingPrice = p.BuyingPrice,
                    SellingPrice = p.SellingPrice,
                    ReorderLevel = p.ReorderLevel,
                    CategoryName = p.StockCategory.CategoryName,
                    SubCategoryName = p.StockSubCategory.SubCategoryName,
                    MeasureName = p.StockMeasure.MeasureName,
                    VatName = p.VatSetup.VatName,
                    ImagePath = p.ImagePath
                })
                .ToListAsync();

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            return View(productDtos);
        }

        // ============================
        // CREATE (GET)
        // ============================
        public IActionResult Create()
        {
            LoadDropdowns();
            return View();
        }

        // ============================
        // CREATE (POST)
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LoadDropdowns();
                    return View(productDto);
                }

                // ---------------------------
                // IMAGE VALIDATION
                // ---------------------------
                if (productDto.ImageFile == null || productDto.ImageFile.Length == 0)
                {
                    ModelState.AddModelError("ImageFile", "Product image is required.");
                    LoadDropdowns();
                    return View(productDto);
                }

                // ---------------------------
                // SAVE IMAGE
                // ---------------------------
                string uploadsFolder = Path.Combine(environment.WebRootPath, "images/products");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff")
                                     + Path.GetExtension(productDto.ImageFile.FileName);

                string imageFullPath = Path.Combine(uploadsFolder, newFileName);

                using (var stream = new FileStream(imageFullPath, FileMode.Create))
                {
                    await productDto.ImageFile.CopyToAsync(stream);
                }

                // ---------------------------
                // CREATE PRODUCT
                // ---------------------------
                var product = new Product
                {
                    ProductName = productDto.ProductName,
                    Barcode = productDto.Barcode,
                    IsActive = productDto.IsActive,
                    StockCategoryId = productDto.StockCategoryId,
                    StockSubCategoryId = productDto.StockSubCategoryId,
                    StockMeasureId = productDto.StockMeasureId,
                    VatSetupId = productDto.VatSetupId,
                    BuyingPrice = productDto.BuyingPrice,
                    SellingPrice = productDto.SellingPrice,
                    ReorderLevel = productDto.ReorderLevel,
                    ImagePath = "/images/products/" + newFileName
                };

                context.Products.Add(product);
                await context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Product created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error creating product: " + ex.Message;
                LoadDropdowns();
                return View(productDto);
            }
        }

        // ============================
        // AJAX: Get SubCategories by Category
        // ============================
        [HttpGet]
        public async Task<JsonResult> GetSubCategories(int categoryId)
        {
            var subcategories = await context.StockSubCategories
                .Where(sc => sc.StockCategoryId == categoryId)
                .Select(sc => new
                {
                    stockSubCategoryId = sc.StockSubCategoryId,
                    subCategoryName = sc.SubCategoryName
                })
                .ToListAsync();

            return Json(subcategories);
        }

        // ============================
        // DROPDOWNS
        // ============================
        private void LoadDropdowns()
        {
            // Categories dropdown
            ViewBag.StockCategoryId = new SelectList(
                context.StockCategories.OrderBy(c => c.CategoryName),
                "StockCategoryId",
                "CategoryName"
            );

            // Subcategories dropdown empty initially — AJAX will populate dynamically
            ViewBag.StockSubCategoryId = new SelectList(
                Enumerable.Empty<SelectListItem>(),
                "Value",
                "Text"
            );

            // Measures dropdown
            ViewBag.StockMeasureId = new SelectList(
                context.StockMeasures.OrderBy(m => m.MeasureName),
                "StockMeasureId",
                "MeasureName"
            );

            // VAT setup dropdown
            ViewBag.VatSetupId = new SelectList(
                context.VatSetups.OrderBy(v => v.VatName),
                "VatSetupId",
                "VatName"
            );
        }
    }
}