using Biashara_POS.Data;
using Biashara_POS.DTOs.UserGroup;
using Biashara_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biashara_POS.Controllers
{
    public class UserGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserGroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ===============================
        // GET: UserGroups
        // ===============================
        public async Task<IActionResult> Index()
        {
            var groups = await _context.UserGroups
                .Select(g => new UserGroupDto
                {
                    UserGroupId = g.UserGroupId,
                    GroupName = g.GroupName,
                    Description = g.Description,
                    IsEditable = g.IsEditable,
                    UserCount = g.Users.Count,
                    PermissionCount = g.GroupPermissions.Count
                })
                .ToListAsync();

            return View(groups);
        }

        // ===============================
        // GET: Create
        // ===============================
        public IActionResult Create()
        {
            return View();
        }

        // ===============================
        // POST: Create
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserGroupDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var group = new UserGroup
            {
                GroupName = dto.GroupName,
                Description = dto.Description
            };

            _context.UserGroups.Add(group);
            await _context.SaveChangesAsync();

            TempData["success"] = "User group created successfully.";

            return RedirectToAction(nameof(Index));
        }

        // ===============================
        // GET: Edit
        // ===============================
        public async Task<IActionResult> Edit(int id)
        {
            var group = await _context.UserGroups.FindAsync(id);

            if (group == null)
                return NotFound();

            var dto = new UpdateUserGroupDto
            {
                UserGroupId = group.UserGroupId,
                GroupName = group.GroupName,
                Description = group.Description
            };

            return View(dto);
        }

        // ===============================
        // POST: Edit
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateUserGroupDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var group = await _context.UserGroups.FindAsync(dto.UserGroupId);

            if (group == null)
                return NotFound();

            if (!group.IsEditable)
            {
                TempData["error"] = "This group cannot be edited.";
                return RedirectToAction(nameof(Index));
            }

            group.GroupName = dto.GroupName;
            group.Description = dto.Description;

            await _context.SaveChangesAsync();

            TempData["success"] = "User group updated successfully.";

            return RedirectToAction(nameof(Index));
        }

        // ===============================
        // DELETE
        // ===============================
        public async Task<IActionResult> Delete(int id)
        {
            var group = await _context.UserGroups.FindAsync(id);

            if (group == null)
                return NotFound();

            if (!group.IsEditable)
            {
                TempData["error"] = "This group cannot be deleted.";
                return RedirectToAction(nameof(Index));
            }

            _context.UserGroups.Remove(group);
            await _context.SaveChangesAsync();

            TempData["success"] = "User group deleted.";

            return RedirectToAction(nameof(Index));
        }
    }
}