using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biashara_POS.Data;
using Biashara_POS.Models;
using Biashara_POS.DTOs;

namespace Biashara_POS.Controllers
{
    public class StockMeasureController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StockMeasureController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ===============================
        // INDEX (LIST ALL)
        // ===============================
        public async Task<IActionResult> Index()
        {
            var measures = await _context.StockMeasures
                .Select(sm => new StockMeasureDto
                {
                    StockMeasureId = sm.StockMeasureId,
                    MeasureName = sm.MeasureName,
                    Initials = sm.Initials
                })
                .ToListAsync();

            return View(measures);
        }

        // ===============================
        // GET: CREATE
        // ===============================
        public IActionResult Create()
        {
            return View();
        }

        // ===============================
        // POST: CREATE
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStockMeasureDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var entity = new StockMeasure
            {
                MeasureName = dto.MeasureName.Trim(),
                Initials = dto.Initials.Trim().ToUpper()
            };

            _context.StockMeasures.Add(entity);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Stock Measure created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // ===============================
        // GET: EDIT
        // ===============================
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _context.StockMeasures.FindAsync(id);

            if (entity == null)
                return NotFound();

            var dto = new UpdateStockMeasureDto
            {
                StockMeasureId = entity.StockMeasureId,
                MeasureName = entity.MeasureName,
                Initials = entity.Initials
            };

            return View(dto);
        }

        // ===============================
        // POST: EDIT
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateStockMeasureDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var entity = await _context.StockMeasures.FindAsync(dto.StockMeasureId);

            if (entity == null)
                return NotFound();

            entity.MeasureName = dto.MeasureName.Trim();
            entity.Initials = dto.Initials.Trim().ToUpper();

            _context.Update(entity);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Stock Measure updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // ===============================
        // GET: DELETE
        // ===============================
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.StockMeasures.FindAsync(id);

            if (entity == null)
                return NotFound();

            var dto = new StockMeasureDto
            {
                StockMeasureId = entity.StockMeasureId,
                MeasureName = entity.MeasureName,
                Initials = entity.Initials
            };

            return View(dto);
        }

        // ===============================
        // POST: DELETE
        // ===============================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entity = await _context.StockMeasures.FindAsync(id);

            if (entity == null)
                return NotFound();

            _context.StockMeasures.Remove(entity);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Stock Measure deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}