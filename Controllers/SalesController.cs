using Biashara_POS.Data;
using Biashara_POS.DTOs;
using Biashara_POS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Biashara_POS.Controllers
{
    [Authorize] // POS requires login
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public SalesController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // =========================
        // SALES LIST
        // =========================
        public async Task<IActionResult> Index()
        {
            var sales = await _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.User)
                .Select(s => new SaleIndexDto
                {
                    SaleId = s.SaleId,
                    ReceiptNumber = s.ReceiptNumber,
                    SaleDate = s.SaleDate,
                    CustomerName = s.Customer != null ? s.Customer.FullName : "Walk-in",
                    TotalAmount = s.TotalAmount,
                    Balance = s.Balance,
                    IsCreditSale = s.IsCreditSale,
                    Cashier = s.User != null ? s.User.FullName : "Unknown"
                })
                .OrderByDescending(s => s.SaleDate)
                .ToListAsync();

            return View(sales);
        }

        // =========================
        // CREATE SALE
        // =========================
        public IActionResult Create()
        {
            ViewBag.Customers = new SelectList(
                _context.Customers.Where(c => c.IsActive),
                "CustomerId",
                "FullName"
            );

            return View();
        }

        // =========================
        // CREATE POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSaleDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Customers = new SelectList(
                    _context.Customers,
                    "CustomerId",
                    "FullName"
                );

                return View(dto);
            }

            // Get logged in cashier
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var sale = new Sale
            {
                ReceiptNumber = Guid.NewGuid().ToString(),
                SaleDate = DateTime.Now,
                CustomerId = dto.CustomerId,
                IsCreditSale = dto.IsCreditSale,
                TotalAmount = dto.TotalAmount,
                Balance = dto.IsCreditSale ? dto.TotalAmount : 0,
                UserId = userId
            };

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // EDIT SALE
        // =========================
        public async Task<IActionResult> Edit(int id)
        {
            var sale = await _context.Sales.FindAsync(id);

            if (sale == null)
                return NotFound();

            var dto = new SaleEditDto
            {
                SaleId = sale.SaleId,
                CustomerId = sale.CustomerId,
                IsCreditSale = sale.IsCreditSale,
                TotalAmount = sale.TotalAmount,
                Balance = sale.Balance
            };

            ViewBag.Customers = new SelectList(
                _context.Customers,
                "CustomerId",
                "FullName",
                sale.CustomerId
            );

            return View(dto);
        }

        // =========================
        // EDIT POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SaleEditDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Customers = new SelectList(
                    _context.Customers,
                    "CustomerId",
                    "FullName"
                );

                return View(dto);
            }

            var sale = await _context.Sales.FindAsync(dto.SaleId);

            if (sale == null)
                return NotFound();

            sale.CustomerId = dto.CustomerId;
            sale.IsCreditSale = dto.IsCreditSale;
            sale.TotalAmount = dto.TotalAmount;
            sale.Balance = dto.Balance;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}