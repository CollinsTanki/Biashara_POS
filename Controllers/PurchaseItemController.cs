using Biashara_POS.Data;
using Biashara_POS.DTOs;
using Biashara_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biashara_POS.Controllers
{
    public class PurchaseItemController : Controller
    {
        private readonly ApplicationDbContext context;

        public PurchaseItemController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // =========================
        // GET: Purchase Items
        // =========================
        public async Task<IActionResult> Index()
        {
            var items = await context.PurchaseItems
                .Include(p => p.Product)
                .Select(p => new PurchaseItemViewDto
                {
                    PurchaseItemId = p.PurchaseItemId,
                    PurchaseId = p.PurchaseId,
                    ProductName = p.Product.ProductName,
                    Quantity = p.Quantity,
                    UnitPrice = p.UnitPrice,
                    TotalPrice = p.Quantity * p.UnitPrice
                })
                .ToListAsync();

            return View(items);
        }

        // =========================
        // GET: Create
        // =========================
        public async Task<IActionResult> Create()
        {
            ViewBag.Products = await context.Products.ToListAsync();
            ViewBag.Purchases = await context.Purchases.ToListAsync();

            return View();
        }

        // =========================
        // POST: Create
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseItemCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                var item = new PurchaseItem
                {
                    PurchaseId = dto.PurchaseId,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    UnitPrice = dto.UnitPrice
                };

                context.PurchaseItems.Add(item);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Products = await context.Products.ToListAsync();
            ViewBag.Purchases = await context.Purchases.ToListAsync();

            return View(dto);
        }

        // =========================
        // GET: Edit
        // =========================
        public async Task<IActionResult> Edit(int id)
        {
            var item = await context.PurchaseItems.FindAsync(id);

            if (item == null)
                return NotFound();

            var dto = new PurchaseItemEditDto
            {
                PurchaseItemId = item.PurchaseItemId,
                PurchaseId = item.PurchaseId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            };

            ViewBag.Products = await context.Products.ToListAsync();
            ViewBag.Purchases = await context.Purchases.ToListAsync();

            return View(dto);
        }

        // =========================
        // POST: Edit
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PurchaseItemEditDto dto)
        {
            if (id != dto.PurchaseItemId)
                return NotFound();

            if (ModelState.IsValid)
            {
                var item = await context.PurchaseItems.FindAsync(id);

                if (item == null)
                    return NotFound();

                item.PurchaseId = dto.PurchaseId;
                item.ProductId = dto.ProductId;
                item.Quantity = dto.Quantity;
                item.UnitPrice = dto.UnitPrice;

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Products = await context.Products.ToListAsync();
            ViewBag.Purchases = await context.Purchases.ToListAsync();

            return View(dto);
        }

        // =========================
        // GET: Delete
        // =========================
        public async Task<IActionResult> Delete(int id)
        {
            var item = await context.PurchaseItems
                .Include(p => p.Product)
                .FirstOrDefaultAsync(p => p.PurchaseItemId == id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        // =========================
        // POST: Delete
        // =========================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await context.PurchaseItems.FindAsync(id);

            if (item != null)
            {
                context.PurchaseItems.Remove(item);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}