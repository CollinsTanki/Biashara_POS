using Biashara_POS.Data;
using Biashara_POS.DTOs;
using Biashara_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biashara_POS.Controllers
{
    public class PaymentModeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentModeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PaymentMode
        public async Task<IActionResult> Index()
        {
            var modes = await _context.PaymentModes
                .Select(pm => new PaymentModeDto
                {
                    PaymentModeId = pm.PaymentModeId,
                    ModeName = pm.ModeName,
                    IsActive = pm.IsActive
                }).ToListAsync();

            return View(modes);
        }

        // GET: PaymentMode/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentMode/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentModeDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var mode = new PaymentMode
            {
                ModeName = dto.ModeName,
                IsActive = dto.IsActive
            };

            _context.PaymentModes.Add(mode);
            await _context.SaveChangesAsync(); // Data saved here
            return RedirectToAction(nameof(Index));
        }

        // GET: PaymentMode/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var mode = await _context.PaymentModes.FindAsync(id);
            if (mode == null) return NotFound();

            var dto = new PaymentModeDto
            {
                PaymentModeId = mode.PaymentModeId,
                ModeName = mode.ModeName,
                IsActive = mode.IsActive
            };

            return View(dto);
        }

        // POST: PaymentMode/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PaymentModeDto dto)
        {
            if (id != dto.PaymentModeId) return BadRequest();
            if (!ModelState.IsValid) return View(dto);

            var mode = await _context.PaymentModes.FindAsync(id);
            if (mode == null) return NotFound();

            mode.ModeName = dto.ModeName;
            mode.IsActive = dto.IsActive;

            await _context.SaveChangesAsync(); // Data updated here
            return RedirectToAction(nameof(Index));
        }

        // GET: PaymentMode/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var mode = await _context.PaymentModes.FindAsync(id);
            if (mode == null) return NotFound();

            _context.PaymentModes.Remove(mode);
            await _context.SaveChangesAsync(); // Data deleted here
            return RedirectToAction(nameof(Index));
        }
    }
}