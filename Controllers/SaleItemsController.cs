using Biashara_POS.Data;
using Biashara_POS.DTOs;
using Biashara_POS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Biashara_POS.Controllers
{
    [Authorize]
    public class SaleItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SaleItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ======================
        // LIST ITEMS
        // ======================
        public async Task<IActionResult> Index(int saleId)
        {
            var items = await _context.SaleItems
                .Include(x => x.Product)
                .Where(x => x.SaleId == saleId)
                .Select(x => new SaleItemIndexDto
                {
                    SaleItemId = x.SaleItemId,
                    SaleId = x.SaleId,
                    ProductName = x.Product.ProductName,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    Discount = x.Discount,
                    VatAmount = x.VatAmount,
                    SubTotal = x.SubTotal
                })
                .ToListAsync();

            ViewBag.SaleId = saleId;

            return View(items);
        }

        // ======================
        // CREATE
        // ======================
        public IActionResult Create(int saleId)
        {
            ViewBag.Products = new SelectList(_context.Products, "ProductId", "Name");
            ViewBag.SaleId = saleId;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaleItemCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Products = new SelectList(_context.Products, "ProductId", "Name");
                return View(dto);
            }

            var subtotal = (dto.Quantity * dto.UnitPrice) - dto.Discount + dto.VatAmount;

            var item = new SaleItem
            {
                SaleId = dto.SaleId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice,
                Discount = dto.Discount,
                VatAmount = dto.VatAmount,
                SubTotal = subtotal
            };

            _context.SaleItems.Add(item);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { saleId = dto.SaleId });
        }

        // ======================
        // EDIT
        // ======================
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.SaleItems.FindAsync(id);

            if (item == null)
                return NotFound();

            var dto = new SaleItemEditDto
            {
                SaleItemId = item.SaleItemId,
                SaleId = item.SaleId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Discount = item.Discount,
                VatAmount = item.VatAmount
            };

            ViewBag.Products = new SelectList(_context.Products, "ProductId", "Name", item.ProductId);

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SaleItemEditDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Products = new SelectList(_context.Products, "ProductId", "Name");
                return View(dto);
            }

            var item = await _context.SaleItems.FindAsync(dto.SaleItemId);

            if (item == null)
                return NotFound();

            item.ProductId = dto.ProductId;
            item.Quantity = dto.Quantity;
            item.UnitPrice = dto.UnitPrice;
            item.Discount = dto.Discount;
            item.VatAmount = dto.VatAmount;

            item.SubTotal = (dto.Quantity * dto.UnitPrice) - dto.Discount + dto.VatAmount;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { saleId = dto.SaleId });
        }
    }
}