using Biashara_POS.Data;
using Biashara_POS.DTOs;
using Biashara_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Biashara_POS.Controllers
{
    public class BranchController : Controller
    {
        private readonly ApplicationDbContext context;

        public BranchController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var branches = context.Branches
                                  .Include(b => b.Company)
                                  .OrderByDescending(b => b.BranchId)
                                  .ToList();

            return View(branches);
        }
        public IActionResult Create()
        {
            ViewBag.Companies = new SelectList(context.Companies, "CompanyId", "CompanyName");
            return View(new BranchDto());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BranchDto dto)
        {
            ViewBag.Companies = new SelectList(context.Companies, "CompanyId", "CompanyName");

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please correct the errors and try again.";
                return View(dto);
            }

            try
            {
                var branch = new Branch
                {
                    CompanyId = dto.CompanyId,
                    BranchName = dto.BranchName,
                    Address = dto.Address,
                    Phone = dto.Phone,
                    Email = dto.Email,
                    Location = dto.Location,
                    IsHQ = dto.IsHQ
                };

                context.Branches.Add(branch);
                context.SaveChanges();

                // ✅ Set success message in TempData
                TempData["SuccessMessage"] = $"Branch '{branch.BranchName}' created successfully!";
                return RedirectToAction(nameof(Index)); // Redirect to Index page
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while creating the branch. Please try again.";
                return View(dto);
            }
        }    
    }
}
