using Biashara_POS.Data;
using Biashara_POS.Models;
using Biashara_POS.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Biashara_POS.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly ApplicationDbContext context;

        public PurchaseController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // ======================
        // GET: Purchases
        // ======================
        public async Task<IActionResult> Index()
        {
            var purchases = await context.Purchases
                .Include(p => p.Supplier)
                .Select(p => new PurchaseViewDto
                {
                    PurchaseId = p.PurchaseId,
                    PurchaseDate = p.PurchaseDate,
                    SupplierName = p.Supplier.SupplierName,
                    TotalAmount = p.TotalAmount,
                    IsCredit = p.IsCredit,
                    ItemCount = p.PurchaseItems.Count()
                })
                .ToListAsync();

            return View(purchases);
        }

        // ======================
        // GET: Create
        // ======================
        public async Task<IActionResult> Create()
        {
            await LoadSuppliers();
            return View();
        }

        // ======================
        // POST: Create
        // ======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadSuppliers(dto.SupplierId);
                return View(dto);
            }

            var purchase = new Purchase
            {
                SupplierId = dto.SupplierId,
                PurchaseDate = dto.PurchaseDate,
                TotalAmount = dto.TotalAmount,
                IsCredit = dto.IsCredit
            };

            context.Purchases.Add(purchase);

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ======================
        // GET: Edit
        // ======================
        public async Task<IActionResult> Edit(int id)
        {
            var purchase = await context.Purchases.FindAsync(id);

            if (purchase == null)
                return NotFound();

            var dto = new PurchaseEditDto
            {
                PurchaseId = purchase.PurchaseId,
                SupplierId = purchase.SupplierId,
                PurchaseDate = purchase.PurchaseDate,
                TotalAmount = purchase.TotalAmount,
                IsCredit = purchase.IsCredit
            };

            await LoadSuppliers(dto.SupplierId);

            return View(dto);
        }

        // ======================
        // POST: Edit
        // ======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PurchaseEditDto dto)
        {
            if (id != dto.PurchaseId)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await LoadSuppliers(dto.SupplierId);
                return View(dto);
            }

            var purchase = await context.Purchases.FindAsync(id);

            if (purchase == null)
                return NotFound();

            purchase.SupplierId = dto.SupplierId;
            purchase.PurchaseDate = dto.PurchaseDate;
            purchase.TotalAmount = dto.TotalAmount;
            purchase.IsCredit = dto.IsCredit;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ======================
        // GET: Delete
        // ======================
        public async Task<IActionResult> Delete(int id)
        {
            var purchase = await context.Purchases
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.PurchaseId == id);

            if (purchase == null)
                return NotFound();

            return View(purchase);
        }

        // ======================
        // POST: Delete
        // ======================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchase = await context.Purchases.FindAsync(id);

            if (purchase != null)
            {
                context.Purchases.Remove(purchase);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // ======================
        // Helper Method
        // ======================
        private async Task LoadSuppliers(int? selectedSupplier = null)
        {
            var suppliers = await context.Suppliers.ToListAsync();

            ViewBag.Suppliers = new SelectList(
                suppliers,
                "SupplierId",
                "SupplierName",
                selectedSupplier
            );
        }
    }
}