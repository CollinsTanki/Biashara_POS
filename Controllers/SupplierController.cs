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

        // =============================
        // GET: Supplier List (Search + Pagination)
        // =============================
        public async Task<IActionResult> Index(string searchString, int page = 1)
        {
            int pageSize = 10;

            var query = context.Suppliers.AsQueryable();

            // Search functionality
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(s =>
                    s.SupplierName.Contains(searchString) ||
                    s.PhoneNumber.Contains(searchString) ||
                    s.Email.Contains(searchString));
            }

            var totalSuppliers = await query.CountAsync();

            var suppliers = await query
                .Select(s => new SupplierViewDto
                {
                    SupplierId = s.SupplierId,
                    SupplierName = s.SupplierName,
                    PhoneNumber = s.PhoneNumber,
                    Email = s.Email,
                    Location = s.Location,
                    Address = s.Address,
                    PurchaseCount = s.Purchases.Count(),
                    Balance = 0
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalSuppliers / (double)pageSize);
            ViewBag.SearchString = searchString;

            return View(suppliers);
        }

        // =============================
        // GET: Supplier/Create
        // =============================
        public IActionResult Create()
        {
            return View();
        }

        // =============================
        // POST: Supplier/Create
        // =============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                Supplier supplier = new Supplier
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

        // =============================
        // GET: Supplier/Details/5
        // =============================
        public async Task<IActionResult> Details(int id)
        {
            var supplier = await context.Suppliers
                .Where(s => s.SupplierId == id)
                .Select(s => new SupplierViewDto
                {
                    SupplierId = s.SupplierId,
                    SupplierName = s.SupplierName,
                    PhoneNumber = s.PhoneNumber,
                    Email = s.Email,
                    Location = s.Location,
                    Address = s.Address,
                    PurchaseCount = s.Purchases.Count(),
                    Balance = 0
                })
                .FirstOrDefaultAsync();

            if (supplier == null)
                return NotFound();

            return View(supplier);
        }

        // =============================
        // GET: Supplier/Edit/5
        // =============================
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

        // =============================
        // POST: Supplier/Edit/5
        // =============================
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

        // =============================
        // GET: Supplier/Delete/5
        // =============================
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await context.Suppliers
                .Where(s => s.SupplierId == id)
                .Select(s => new SupplierViewDto
                {
                    SupplierId = s.SupplierId,
                    SupplierName = s.SupplierName,
                    PhoneNumber = s.PhoneNumber,
                    Email = s.Email,
                    Location = s.Location,
                    Address = s.Address
                })
                .FirstOrDefaultAsync();

            if (supplier == null)
                return NotFound();

            return View(supplier);
        }

        // =============================
        // POST: Supplier/Delete
        // =============================
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