using Biashara_POS.DTOs.Users;
using Biashara_POS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biashara_POS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // ===============================
        // USERS LIST
        // ===============================

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users
                .Select(u => new UserIndexDto
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Email = u.Email!,
                    Position = u.Position!,
                    IsActive = u.IsActive,
                    Branch = u.Branch != null ? u.Branch.BranchName : ""
                })
                .ToListAsync();

            return View(users);
        }

        // ===============================
        // CREATE USER
        // ===============================

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var user = new AppUser
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.Email,
                Position = dto.Position,
                Address = dto.Address,
                BranchId = dto.BranchId,
                UserGroupId = dto.UserGroupId,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(dto);
        }

        // ===============================
        // EDIT USER
        // ===============================

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var dto = new UserEditDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email!,
                Position = user.Position,
                Address = user.Address,
                BranchId = user.BranchId,
                UserGroupId = user.UserGroupId,
                IsActive = user.IsActive
            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var user = await _userManager.FindByIdAsync(dto.Id);

            if (user == null)
                return NotFound();

            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.UserName = dto.Email;
            user.Position = dto.Position;
            user.Address = dto.Address;
            user.BranchId = dto.BranchId;
            user.UserGroupId = dto.UserGroupId;
            user.IsActive = dto.IsActive;

            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }

        // ===============================
        // DELETE USER
        // ===============================

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }
    }
}