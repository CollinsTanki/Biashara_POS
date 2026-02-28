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

        public ProductController(ApplicationDbContext context)
        {
            this.context = context;
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

                    // Flattened fields
                    CategoryName = p.StockCategory.CategoryName,
                    SubCategoryName = p.StockSubCategory.SubCategoryName,
                    MeasureName = p.StockMeasure.MeasureName,
                    VatName = p.VatSetup.VatName,

                    // Image
                    ImagePath = p.ImagePath
                })
                .ToListAsync();

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
            if (!ModelState.IsValid)
            {
                LoadDropdowns();
                return View(productDto);
            }

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
                ReorderLevel = productDto.ReorderLevel
            };

            // -------------------------
            // HANDLE IMAGE UPLOAD
            // -------------------------
            if (productDto.ImageFile != null && productDto.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products");
                Directory.CreateDirectory(uploadsFolder); // ensure folder exists

                // create a unique filename to prevent collisions
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(productDto.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await productDto.ImageFile.CopyToAsync(stream);
                }

                // store relative path in DB
                product.ImagePath = "/images/products/" + uniqueFileName;
            }

            context.Products.Add(product);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ============================
        // DROPDOWN LOADER
        // ============================
        private void LoadDropdowns()
        {
            ViewBag.StockCategoryId = new SelectList(
                context.StockCategories,
                "StockCategoryId",
                "CategoryName"
            );

            ViewBag.StockSubCategoryId = new SelectList(
                context.StockSubCategories,
                "StockSubCategoryId",
                "SubCategoryName"
            );

            ViewBag.StockMeasureId = new SelectList(
                context.StockMeasures,
                "StockMeasureId",
                "MeasureName"
            );

            ViewBag.VatSetupId = new SelectList(
                context.VatSetups,
                "VatSetupId",
                "VatName"
            );
        }
    }
}