using Biashara_POS.Data;
using Biashara_POS.DTOs;
using Biashara_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Biashara_POS.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Payment
        public async Task<IActionResult> Index()
        {
            var payments = await _context.Payments
                .Include(p => p.PaymentMode)
                .Select(p => new PaymentDto
                {
                    PaymentId = p.PaymentId,
                    SaleId = p.SaleId,
                    PaymentModeId = p.PaymentModeId,
                    PaymentModeName = p.PaymentMode!.ModeName,
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    ReferenceNumber = p.ReferenceNumber
                }).ToListAsync();

            return View(payments);
        }

        // GET: Payment/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.PaymentModes = new SelectList(
                await _context.PaymentModes.ToListAsync(),
                "PaymentModeId",
                "ModeName"
            );

            return View();
        }

        // POST: Payment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePaymentDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PaymentModes = new SelectList(_context.PaymentModes, "PaymentModeId", "ModeName");
                return View(dto);
            }

            var payment = new Payment
            {
                SaleId = dto.SaleId,
                PaymentModeId = dto.PaymentModeId,
                Amount = dto.Amount,
                ReferenceNumber = dto.ReferenceNumber
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // DELETE
        public async Task<IActionResult> Delete(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return NotFound();

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}