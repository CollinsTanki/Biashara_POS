using Biashara_POS.Data;
using Biashara_POS.DTOs;
using Biashara_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                .Include(p => p.Purchase)
                .Select(p => new PurchaseItemViewDto
                {
                    PurchaseItemId = p.PurchaseItemId,
                    PurchaseNumber = p.Purchase.PurchaseNumber,
                    ProductName = p.Product.ProductName,
                    Quantity = p.Quantity,
                    UnitPrice = p.UnitPrice,
                    Total = p.Quantity * p.UnitPrice
                })
                .ToListAsync();

            return View(items);
        }

        // =========================
        // GET: Create
        // =========================
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
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

                // Update product stock
                var product = await context.Products.FindAsync(dto.ProductId);
                if (product != null)
                    product.StockQuantity += dto.Quantity;

                await context.SaveChangesAsync();

                // Update purchase total
                await UpdatePurchaseTotal(dto.PurchaseId);

                return RedirectToAction(nameof(Index));
            }

            // Reload dropdowns if validation fails
            await LoadDropdownsAsync();
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

            await LoadDropdownsAsync();
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

                // Adjust stock based on change
                var product = await context.Products.FindAsync(dto.ProductId);
                if (product != null)
                    product.StockQuantity = product.StockQuantity - item.Quantity + dto.Quantity;

                item.PurchaseId = dto.PurchaseId;
                item.ProductId = dto.ProductId;
                item.Quantity = dto.Quantity;
                item.UnitPrice = dto.UnitPrice;

                await context.SaveChangesAsync();

                await UpdatePurchaseTotal(item.PurchaseId);

                return RedirectToAction(nameof(Index));
            }

            await LoadDropdownsAsync();
            return View(dto);
        }

        // =========================
        // GET: Delete
        // =========================
        public async Task<IActionResult> Delete(int id)
        {
            var item = await context.PurchaseItems
                .Include(p => p.Product)
                .Include(p => p.Purchase)
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
                var product = await context.Products.FindAsync(item.ProductId);
                if (product != null)
                    product.StockQuantity -= item.Quantity;

                int purchaseId = item.PurchaseId;
                context.PurchaseItems.Remove(item);
                await context.SaveChangesAsync();

                await UpdatePurchaseTotal(purchaseId);
            }

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // PRIVATE: Update Purchase Total
        // =========================
        private async Task UpdatePurchaseTotal(int purchaseId)
        {
            var purchase = await context.Purchases
                .Include(p => p.PurchaseItems)
                .FirstOrDefaultAsync(p => p.PurchaseId == purchaseId);

            if (purchase != null)
            {
                purchase.TotalAmount = purchase.PurchaseItems.Sum(i => i.Quantity * i.UnitPrice);
                await context.SaveChangesAsync();
            }
        }

        // =========================
        // PRIVATE: Load dropdowns for Create/Edit
        // =========================
        private async Task LoadDropdownsAsync()
        {
            // Map Products to SelectList
            ViewBag.Products = new SelectList(
                await context.Products.OrderBy(p => p.ProductName).ToListAsync(),
                "ProductId",
                "ProductName"
            );

            // Map Purchases to SelectList
            ViewBag.Purchases = new SelectList(
                await context.Purchases.OrderByDescending(p => p.PurchaseDate).ToListAsync(),
                "PurchaseId",
                "PurchaseNumber"
            );
        }
    }
}