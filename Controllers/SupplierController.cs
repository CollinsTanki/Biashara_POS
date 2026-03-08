using Biashara_POS.Data;
using Biashara_POS.Models;
using Biashara_POS.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biashara_POS.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext context;

        public SupplierController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // ======================
        // GET: Suppliers List
        // ======================
        public async Task<IActionResult> Index()
        {
            var suppliers = await context.Suppliers
                .Select(s => new SupplierViewDto
                {
                    SupplierId = s.SupplierId,
                    SupplierName = s.SupplierName,
                    PhoneNumber = s.PhoneNumber,
                    Email = s.Email,
                    Location = s.Location,
                    Address = s.Address
                })
                .ToListAsync();

            return View(suppliers);
        }

        // ======================
        // GET: Create Supplier
        // ======================
        public IActionResult Create()
        {
            return View(new SupplierCreateDto());
        }

        // ======================
        // POST: Create Supplier
        // ======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                var supplier = new Supplier
                {
                    SupplierName = dto.SupplierName,
                    PhoneNumber = dto.PhoneNumber,
                    Email = dto.Email,
                    Location = dto.Location,
                    Address = dto.Address
                };

                context.Suppliers.Add(supplier);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        // ======================
        // GET: Edit Supplier
        // ======================
        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await context.Suppliers.FindAsync(id);

            if (supplier == null)
                return NotFound();

            var dto = new SupplierEditDto
            {
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                PhoneNumber = supplier.PhoneNumber,
                Email = supplier.Email,
                Location = supplier.Location,
                Address = supplier.Address
            };

            return View(dto);
        }

        // ======================
        // POST: Edit Supplier
        // ======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SupplierEditDto dto)
        {
            if (id != dto.SupplierId)
                return NotFound();

            if (ModelState.IsValid)
            {
                var supplier = await context.Suppliers.FindAsync(id);

                if (supplier == null)
                    return NotFound();

                supplier.SupplierName = dto.SupplierName;
                supplier.PhoneNumber = dto.PhoneNumber;
                supplier.Email = dto.Email;
                supplier.Location = dto.Location;
                supplier.Address = dto.Address;

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        // ======================
        // GET: Delete Supplier
        // ======================
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await context.Suppliers
                .FirstOrDefaultAsync(s => s.SupplierId == id);

            if (supplier == null)
                return NotFound();

            return View(supplier);
        }

        // ======================
        // POST: Delete Supplier
        // ======================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = await context.Suppliers.FindAsync(id);

            if (supplier != null)
            {
                context.Suppliers.Remove(supplier);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}