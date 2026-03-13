using Biashara_POS.Data;
using Biashara_POS.DTOs.ModuleDTOs;
using Biashara_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biashara_POS.Controllers
{
    public class ModulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ==========================
        // INDEX
        // ==========================
        public async Task<IActionResult> Index()
        {
            var modules = await _context.Modules
                .Select(m => new ModuleDto
                {
                    ModuleId = m.ModuleId,
                    ModuleName = m.ModuleName,
                    FunctionCount = m.Functions.Count
                })
                .ToListAsync();

            return View(modules);
        }

        // ==========================
        // CREATE
        // ==========================
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateModuleDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var module = new Module
            {
                ModuleName = dto.ModuleName
            };

            _context.Modules.Add(module);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ==========================
        // EDIT
        // ==========================
        public async Task<IActionResult> Edit(int id)
        {
            var module = await _context.Modules.FindAsync(id);

            if (module == null)
                return NotFound();

            var dto = new UpdateModuleDto
            {
                ModuleId = module.ModuleId,
                ModuleName = module.ModuleName
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateModuleDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var module = await _context.Modules.FindAsync(dto.ModuleId);

            if (module == null)
                return NotFound();

            module.ModuleName = dto.ModuleName;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ==========================
        // DELETE
        // ==========================
        public async Task<IActionResult> Delete(int id)
        {
            var module = await _context.Modules.FindAsync(id);

            if (module == null)
                return NotFound();

            _context.Modules.Remove(module);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}