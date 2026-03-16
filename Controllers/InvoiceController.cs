using Biashara_POS.Data;
using Biashara_POS.DTOs;
using Biashara_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Biashara_POS.Controllers
{ 

public class InvoiceController : Controller
{
    private readonly ApplicationDbContext _context;

    public InvoiceController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Invoice
    public async Task<IActionResult> Index()
    {
        var invoices = await _context.Invoices
            .Include(i => i.Customer)
            .Select(i => new InvoiceDto
            {
                InvoiceId = i.InvoiceId,
                InvoiceNumber = i.InvoiceNumber,
                InvoiceDate = i.InvoiceDate,
                CustomerId = i.CustomerId,
                CustomerName = i.Customer.FullName,
                SubTotal = i.SubTotal,
                VatTotal = i.VatTotal,
                DiscountTotal = i.DiscountTotal,
                GrandTotal = i.GrandTotal,
                IsPaid = i.IsPaid
            }).ToListAsync();

        return View(invoices);
    }

    // GET: Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Create
    [HttpPost]
    public async Task<IActionResult> Create(InvoiceDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var invoice = new Invoice
        {
            InvoiceNumber = dto.InvoiceNumber,
            InvoiceDate = dto.InvoiceDate,
            CustomerId = dto.CustomerId,
            SubTotal = dto.SubTotal,
            VatTotal = dto.VatTotal,
            DiscountTotal = dto.DiscountTotal,
            GrandTotal = dto.GrandTotal,
            IsPaid = dto.IsPaid
        };

        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: Details
    public async Task<IActionResult> Details(int id)
    {
        var invoice = await _context.Invoices
            .Include(i => i.Customer)
            .Include(i => i.InvoiceItems)
            .ThenInclude(ii => ii.Product)
            .Where(i => i.InvoiceId == id)
            .Select(i => new InvoiceDto
            {
                InvoiceId = i.InvoiceId,
                InvoiceNumber = i.InvoiceNumber,
                InvoiceDate = i.InvoiceDate,
                CustomerName = i.Customer.FullName,
                SubTotal = i.SubTotal,
                VatTotal = i.VatTotal,
                DiscountTotal = i.DiscountTotal,
                GrandTotal = i.GrandTotal,
                IsPaid = i.IsPaid,
                Items = i.InvoiceItems.Select(ii => new InvoiceItemDto
                {
                    InvoiceItemId = ii.InvoiceItemId,
                    ProductId = ii.ProductId,
                    ProductName = ii.Product.ProductName,
                    Quantity = ii.Quantity,
                    UnitPrice = ii.UnitPrice,
                    Total = ii.Total
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (invoice == null)
            return NotFound();

        return View(invoice);
    }
}
}
