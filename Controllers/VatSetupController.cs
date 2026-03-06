using Biashara_POS.Data;
using Biashara_POS.DTOs;
using Biashara_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biashara_POS.Controllers
{
    public class VatSetupController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VatSetupController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // INDEX
        // =========================
        public async Task<IActionResult> Index()
        {
            var vatList = await _context.VatSetups
                .Select(v => new VatSetupViewDto
                {
                    VatSetupId = v.VatSetupId,
                    VatName = v.VatName,
                    VatInitials = v.VatInitials,
                    TaxRate = v.TaxRate,
                    IsActive = v.IsActive
                })
                .ToListAsync();

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            return View(vatList);
        }

        // =========================
        // GET: CREATE
        // =========================
        public IActionResult Create()
        {
            var dto = new VatSetupDto
            {
                IsActive = true // default
            };
            return View(dto);
        }

        // =========================
        // POST: CREATE
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VatSetupDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            // Prevent duplicate initials
            if (await _context.VatSetups.AnyAsync(v => v.VatInitials == dto.VatInitials))
            {
                ModelState.AddModelError("VatInitials", "VAT Initials already exist.");
                return View(dto);
            }

            var vat = new VatSetup
            {
                VatName = dto.VatName,
                VatInitials = dto.VatInitials,
                TaxRate = dto.TaxRate,
                IsActive = dto.IsActive
            };

            _context.VatSetups.Add(vat);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "VAT created successfully!";
            return RedirectToAction(nameof(Index));
        }

        // =========================
        // GET: EDIT
        // =========================
        public async Task<IActionResult> Edit(int id)
        {
            var vat = await _context.VatSetups.FindAsync(id);
            if (vat == null) return NotFound();

            var dto = new VatSetupDto
            {
                VatSetupId = vat.VatSetupId,
                VatName = vat.VatName,
                VatInitials = vat.VatInitials,
                TaxRate = vat.TaxRate,
                IsActive = vat.IsActive
            };

            return View(dto);
        }

        // =========================
        // POST: EDIT
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VatSetupDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            // Prevent duplicate initials on edit
            if (await _context.VatSetups.AnyAsync(v => v.VatInitials == dto.VatInitials && v.VatSetupId != dto.VatSetupId))
            {
                ModelState.AddModelError("VatInitials", "VAT Initials already exist.");
                return View(dto);
            }

            var vat = await _context.VatSetups.FindAsync(dto.VatSetupId);
            if (vat == null) return NotFound();

            vat.VatName = dto.VatName;
            vat.VatInitials = dto.VatInitials;
            vat.TaxRate = dto.TaxRate;
            vat.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "VAT updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        // =========================
        // TOGGLE ACTIVE STATUS
        // =========================
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var vat = await _context.VatSetups.FindAsync(id);
            if (vat == null)
            {
                TempData["ErrorMessage"] = "VAT not found.";
                return RedirectToAction(nameof(Index));
            }

            vat.IsActive = !vat.IsActive;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "VAT status updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        // =========================
        // DELETE
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var vat = await _context.VatSetups.FindAsync(id);
            if (vat == null)
            {
                TempData["ErrorMessage"] = "VAT not found.";
                return RedirectToAction(nameof(Index));
            }

            _context.VatSetups.Remove(vat);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "VAT deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}