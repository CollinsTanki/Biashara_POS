using Biashara_POS.Data;
using Biashara_POS.DTOs;
using Biashara_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Biashara_POS.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LIST
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers
                .OrderBy(c => c.FullName)
                .ToListAsync();

            var result = customers.Select(c => new CustomerDto
            {
                CustomerId = c.CustomerId,
                FullName = c.FullName,
                PhoneNumber = c.PhoneNumber,
                Location = c.Location,
                CreditLimit = c.CreditLimit,
                LoyaltyPoints = c.LoyaltyPoints,
                BalanceBroughtForward = c.BalanceBroughtForward,
                IsActive = c.IsActive,
                IsWalkIn = c.IsWalkIn
            }).ToList();

            return View(result);
        }

        // CREATE VIEW
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCustomerDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var customer = new Customer
            {
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                Location = dto.Location,
                CreditLimit = dto.CreditLimit,
                IsWalkIn = dto.IsWalkIn
            };

            _context.Customers.Add(customer);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // EDIT VIEW
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound();

            var dto = new UpdateCustomerDto
            {
                CustomerId = customer.CustomerId,
                FullName = customer.FullName,
                PhoneNumber = customer.PhoneNumber,
                Location = customer.Location,
                CreditLimit = customer.CreditLimit,
                IsActive = customer.IsActive
            };

            return View(dto);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateCustomerDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var customer = await _context.Customers.FindAsync(dto.CustomerId);

            if (customer == null)
                return NotFound();

            customer.FullName = dto.FullName;
            customer.PhoneNumber = dto.PhoneNumber;
            customer.Location = dto.Location;
            customer.CreditLimit = dto.CreditLimit;
            customer.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // DELETE
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound();

            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}