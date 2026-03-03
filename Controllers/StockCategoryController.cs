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
                return View(dto);

            var entity = new StockCategory
            {
                CategoryName = dto.CategoryName,
                IsActive = true
            };

            _context.StockCategories.Add(entity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // =============================
        // EDIT - GET
        // =============================
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.StockCategories.FindAsync(id);
            if (category == null) return NotFound();

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
                return View(dto);

            var entity = await _context.StockCategories.FindAsync(dto.StockCategoryId);
            if (entity == null) return NotFound();

            entity.CategoryName = dto.CategoryName;
            entity.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}