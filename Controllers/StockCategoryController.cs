using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biashara_POS.Data;
using Biashara_POS.Models;
using Biashara_POS.DTOs;

namespace Biashara_POS.Controllers
{
    public class StockCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StockCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =============================
        // INDEX
        // =============================
        public async Task<IActionResult> Index()
        {
            var categories = await _context.StockCategories
                .Select(x => new StockCategoryDto
                {
                    StockCategoryId = x.StockCategoryId,
                    CategoryName = x.CategoryName,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            return View(categories);
        }

        // =============================
        // CREATE - GET
        // =============================
        public IActionResult Create()
        {
            return View();
        }

        // =============================
        // CREATE - POST
        // =============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStockCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please correct the form errors.";
                return View(dto);
            }

            try
            {
                // 🔥 Duplicate check
                var exists = await _context.StockCategories
                    .AnyAsync(x => x.CategoryName.ToLower() == dto.CategoryName.ToLower());

                if (exists)
                {
                    ModelState.AddModelError("", "Category already exists.");
                    return View(dto);
                }

                var entity = new StockCategory
                {
                    CategoryName = dto.CategoryName.Trim(),
                    IsActive = true
                };

                _context.StockCategories.Add(entity);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Stock Category created successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["Error"] = "Something went wrong while creating the category.";
                return View(dto);
            }
        }

        // =============================
        // EDIT - GET
        // =============================
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.StockCategories.FindAsync(id);
            if (category == null)
            {
                TempData["Error"] = "Category not found.";
                return RedirectToAction(nameof(Index));
            }

            var dto = new StockCategoryDto
            {
                StockCategoryId = category.StockCategoryId,
                CategoryName = category.CategoryName,
                IsActive = category.IsActive
            };

            return View(dto);
        }

        // =============================
        // EDIT - POST
        // =============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StockCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please correct the form errors.";
                return View(dto);
            }

            try
            {
                var entity = await _context.StockCategories.FindAsync(dto.StockCategoryId);
                if (entity == null)
                {
                    TempData["Error"] = "Category not found.";
                    return RedirectToAction(nameof(Index));
                }

                // 🔥 Duplicate check (excluding current record)
                var exists = await _context.StockCategories
                    .AnyAsync(x => x.CategoryName.ToLower() == dto.CategoryName.ToLower()
                                   && x.StockCategoryId != dto.StockCategoryId);

                if (exists)
                {
                    ModelState.AddModelError("", "Another category with this name already exists.");
                    return View(dto);
                }

                entity.CategoryName = dto.CategoryName.Trim();
                entity.IsActive = dto.IsActive;

                await _context.SaveChangesAsync();

                TempData["Success"] = "Stock Category updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["Error"] = "Something went wrong while updating the category.";
                return View(dto);
            }
        }
    }
}