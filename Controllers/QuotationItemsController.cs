using Biashara_POS.Data;
using Biashara_POS.DTOs;
using Biashara_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Biashara_POS.Controllers
{
    public class QuotationItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuotationItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LIST ITEMS
        public async Task<IActionResult> Index(int quotationId)
        {
            var items = await _context.QuotationItems
                .Include(x => x.Product)
                .Where(x => x.QuotationId == quotationId)
                .ToListAsync();

            var result = items.Select(x => new QuotationItemDto
            {
                QuotationItemId = x.QuotationItemId,
                QuotationId = x.QuotationId,
                ProductName = x.Product.ProductName,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                Discount = x.Discount,
                VatAmount = x.VatAmount,
                SubTotal = x.SubTotal
            }).ToList();

            ViewBag.QuotationId = quotationId;

            return View(result);
        }

        // CREATE VIEW
        public async Task<IActionResult> Create(int quotationId)
        {
            ViewBag.Products = await _context.Products.ToListAsync();
            ViewBag.QuotationId = quotationId;

            return View();
        }

        // CREATE POST
        [HttpPost]
        public async Task<IActionResult> Create(CreateQuotationItemDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var subtotal =
                (dto.Quantity * dto.UnitPrice)
                - dto.Discount
                + dto.VatAmount;

            var item = new QuotationItem
            {
                QuotationId = dto.QuotationId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice,
                Discount = dto.Discount,
                VatAmount = dto.VatAmount,
                SubTotal = subtotal
            };

            _context.QuotationItems.Add(item);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { quotationId = dto.QuotationId });
        }

        // EDIT VIEW
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.QuotationItems.FindAsync(id);

            if (item == null)
                return NotFound();

            var dto = new UpdateQuotationItemDto
            {
                QuotationItemId = item.QuotationItemId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Discount = item.Discount,
                VatAmount = item.VatAmount
            };

            return View(dto);
        }

        // EDIT POST
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateQuotationItemDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var item = await _context.QuotationItems.FindAsync(dto.QuotationItemId);

            if (item == null)
                return NotFound();

            item.Quantity = dto.Quantity;
            item.UnitPrice = dto.UnitPrice;
            item.Discount = dto.Discount;
            item.VatAmount = dto.VatAmount;

            item.SubTotal =
                (dto.Quantity * dto.UnitPrice)
                - dto.Discount
                + dto.VatAmount;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { quotationId = item.QuotationId });
        }

        // DELETE
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.QuotationItems.FindAsync(id);

            if (item == null)
                return NotFound();

            int quotationId = item.QuotationId;

            _context.QuotationItems.Remove(item);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { quotationId });
        }
    }
}