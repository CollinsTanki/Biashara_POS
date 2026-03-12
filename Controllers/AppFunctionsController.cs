using Biashara_POS.Data;
using Biashara_POS.DTOs.AppFunction;
using Biashara_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Biashara_POS.Controllers
{
    public class AppFunctionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppFunctionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ============================
        // LIST
        // ============================
        public async Task<IActionResult> Index()
        {
            var functions = await _context.AppFunctions
                .Include(f => f.Module)
                .Select(f => new AppFunctionDto
                {
                    AppFunctionId = f.AppFunctionId,
                    FunctionName = f.FunctionName,
                    ModuleId = f.ModuleId,
                    ModuleName = f.Module.ModuleName
                })
                .ToListAsync();

            return View(functions);
        }

        // ============================
        // CREATE
        // ============================
        public async Task<IActionResult> Create()
        {
            ViewBag.Modules = new SelectList(
                await _context.Modules.ToListAsync(),
                "ModuleId",
                "ModuleName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAppFunctionDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var function = new AppFunction
            {
                FunctionName = dto.FunctionName,
                ModuleId = dto.ModuleId
            };

            _context.AppFunctions.Add(function);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ============================
        // EDIT
        // ============================
        public async Task<IActionResult> Edit(int id)
        {
            var function = await _context.AppFunctions.FindAsync(id);

            if (function == null)
                return NotFound();

            var dto = new UpdateAppFunctionDto
            {
                AppFunctionId = function.AppFunctionId,
                FunctionName = function.FunctionName,
                ModuleId = function.ModuleId
            };

            ViewBag.Modules = new SelectList(
                await _context.Modules.ToListAsync(),
                "ModuleId",
                "ModuleName");

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateAppFunctionDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var function = await _context.AppFunctions.FindAsync(dto.AppFunctionId);

            if (function == null)
                return NotFound();

            function.FunctionName = dto.FunctionName;
            function.ModuleId = dto.ModuleId;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ============================
        // DELETE
        // ============================
        public async Task<IActionResult> Delete(int id)
        {
            var function = await _context.AppFunctions.FindAsync(id);

            if (function == null)
                return NotFound();

            _context.AppFunctions.Remove(function);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}