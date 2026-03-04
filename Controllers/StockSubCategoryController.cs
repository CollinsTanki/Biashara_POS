using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Biashara_POS.Data;
using Biashara_POS.Models;
using Biashara_POS.DTOs.StockSubCategory;
using Biashara_POS.DTOs;

namespace Biashara_POS.Controllers
{
    public class StockSubCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StockSubCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =============================
        // INDEX
        // =============================
        public async Task<IActionResult> Index()
        {
            var subCategories = await _context.StockSubCategories
                .Include(x => x.StockCategory)
                .Select(x => new StockSubCategoryViewDto
                {
                    StockSubCategoryId = x.StockSubCategoryId,
                    SubCategoryName = x.SubCategoryName,
                    IsActive = x.IsActive,
                    StockCategoryId = x.StockCategoryId,
                    CategoryName = x.StockCategory.CategoryName
                })
                .ToListAsync();

            return View(subCategories);
        }

        // =============================
        // CREATE - GET
        // =============================
        public async Task<IActionResult> Create()
        {
            await LoadCategories();
            return View();
        }

        // =============================
        // CREATE - POST
        // =============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStockSubCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please correct the form errors.";
                await LoadCategories();
                return View(dto);
            }

            try
            {
                // Duplicate check
                var exists = await _context.StockSubCategories
                    .AnyAsync(x => x.SubCategoryName.ToLower() == dto.SubCategoryName.ToLower()
                                   && x.StockCategoryId == dto.StockCategoryId);

                if (exists)
                {
                    ModelState.AddModelError("", "Subcategory already exists in the selected category.");
                    await LoadCategories();
                    return View(dto);
                }

                var entity = new StockSubCategory
                {
                    StockCategoryId = dto.StockCategoryId,
                    SubCategoryName = dto.SubCategoryName.Trim(),
                    IsActive = true
                };

                _context.StockSubCategories.Add(entity);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Stock Subcategory created successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["Error"] = "Something went wrong while creating the subcategory.";
                await LoadCategories();
                return View(dto);
            }
        }

        // =============================
        // EDIT - GET
        // =============================
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _context.StockSubCategories.FindAsync(id);
            if (entity == null)
            {
                TempData["Error"] = "Subcategory not found.";
                return RedirectToAction(nameof(Index));
            }

            var dto = new StockSubCategoryDto
            {
                StockSubCategoryId = entity.StockSubCategoryId,
                StockCategoryId = entity.StockCategoryId,
                SubCategoryName = entity.SubCategoryName,
                IsActive = entity.IsActive
            };

            await LoadCategories();
            return View(dto);
        }

        // =============================
        // EDIT - POST
        // =============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StockSubCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please correct the form errors.";
                await LoadCategories();
                return View(dto);
            }

            try
            {
                var entity = await _context.StockSubCategories.FindAsync(dto.StockSubCategoryId);
                if (entity == null)
                {
                    TempData["Error"] = "Subcategory not found.";
                    return RedirectToAction(nameof(Index));
                }

                // Duplicate check (excluding current record)
                var exists = await _context.StockSubCategories
                    .AnyAsync(x => x.SubCategoryName.ToLower() == dto.SubCategoryName.ToLower()
                                   && x.StockCategoryId == dto.StockCategoryId
                                   && x.StockSubCategoryId != dto.StockSubCategoryId);

                if (exists)
                {
                    ModelState.AddModelError("", "Another subcategory with this name already exists in the selected category.");
                    await LoadCategories();
                    return View(dto);
                }

                entity.StockCategoryId = dto.StockCategoryId;
                entity.SubCategoryName = dto.SubCategoryName.Trim();
                entity.IsActive = dto.IsActive;

                await _context.SaveChangesAsync();

                TempData["Success"] = "Stock Subcategory updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["Error"] = "Something went wrong while updating the subcategory.";
                await LoadCategories();
                return View(dto);
            }
        }

        // =============================
        // HELPER
        // =============================
        private async Task LoadCategories()
        {
            ViewBag.Categories = new SelectList(
                await _context.StockCategories
                    .Where(x => x.IsActive)
                    .ToListAsync(),
                "StockCategoryId",
                "CategoryName");
        }
    }
}