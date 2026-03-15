using Biashara_POS.Data;
using Biashara_POS.DTOs;
using Biashara_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Biashara_POS.Controllers
{
    public class QuotationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuotationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LIST
        public async Task<IActionResult> Index()
        {
            var quotations = await _context.Quotations
                .Include(q => q.Customer)
                .OrderByDescending(q => q.CreatedDate)
                .ToListAsync();

            var result = quotations.Select(q => new QuotationDto
            {
                QuotationId = q.QuotationId,
                RefNumber = q.RefNumber,
                CreatedDate = q.CreatedDate,
                ValidUntil = q.ValidUntil,
                CustomerName = q.Customer.FullName,
                TotalAmount = q.TotalAmount,
                IsConfirmed = q.IsConfirmed
            }).ToList();

            return View(result);
        }

        // CREATE VIEW
        public async Task<IActionResult> Create()
        {
            ViewBag.Customers = await _context.Customers.ToListAsync();
            return View();
        }

        // CREATE POST
        [HttpPost]
        public async Task<IActionResult> Create(CreateQuotationDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var quotation = new Quotation
            {
                RefNumber = dto.RefNumber,
                CustomerId = dto.CustomerId,
                ValidUntil = dto.ValidUntil,
                CreatedDate = DateTime.Now
            };

            _context.Quotations.Add(quotation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // EDIT VIEW
        public async Task<IActionResult> Edit(int id)
        {
            var quotation = await _context.Quotations.FindAsync(id);

            if (quotation == null) return NotFound();

            var dto = new UpdateQuotationDto
            {
                QuotationId = quotation.QuotationId,
                ValidUntil = quotation.ValidUntil,
                IsConfirmed = quotation.IsConfirmed
            };

            return View(dto);
        }

        // EDIT POST
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateQuotationDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var quotation = await _context.Quotations.FindAsync(dto.QuotationId);

            if (quotation == null) return NotFound();

            quotation.ValidUntil = dto.ValidUntil;
            quotation.IsConfirmed = dto.IsConfirmed;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // DELETE
        public async Task<IActionResult> Delete(int id)
        {
            var quotation = await _context.Quotations.FindAsync(id);

            if (quotation == null) return NotFound();

            _context.Quotations.Remove(quotation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}