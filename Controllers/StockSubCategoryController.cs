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
                await LoadCategories();
                return View(dto);
            }

            var entity = new StockSubCategory
            {
                StockCategoryId = dto.StockCategoryId,
                SubCategoryName = dto.SubCategoryName,
                IsActive = true
            };

            _context.StockSubCategories.Add(entity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // =============================
        // EDIT - GET
        // =============================
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _context.StockSubCategories.FindAsync(id);
            if (entity == null) return NotFound();

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
                await LoadCategories();
                return View(dto);
            }

            var entity = await _context.StockSubCategories.FindAsync(dto.StockSubCategoryId);
            if (entity == null) return NotFound();

            entity.StockCategoryId = dto.StockCategoryId;
            entity.SubCategoryName = dto.SubCategoryName;
            entity.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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