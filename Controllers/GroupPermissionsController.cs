using Biashara_POS.Data;
using Biashara_POS.DTOs.GroupPermission;
using Biashara_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Biashara_POS.Controllers
{
    public class GroupPermissionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupPermissionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ==============================
        // LIST PERMISSIONS
        // ==============================
        public async Task<IActionResult> Index()
        {
            var permissions = await _context.GroupPermissions
                .Include(g => g.UserGroup)
                .Include(g => g.AppFunction)
                .Select(p => new GroupPermissionDto
                {
                    GroupPermissionId = p.GroupPermissionId,
                    UserGroupId = p.UserGroupId,
                    GroupName = p.UserGroup.GroupName,
                    AppFunctionId = p.AppFunctionId,
                    FunctionName = p.AppFunction.FunctionName,
                    CanView = p.CanView,
                    CanCreate = p.CanCreate,
                    CanEdit = p.CanEdit,
                    CanDelete = p.CanDelete
                })
                .ToListAsync();

            return View(permissions);
        }

        // ==============================
        // CREATE
        // ==============================
        public async Task<IActionResult> Create()
        {
            ViewBag.Groups = new SelectList(await _context.UserGroups.ToListAsync(), "UserGroupId", "GroupName");
            ViewBag.Functions = new SelectList(await _context.AppFunctions.ToListAsync(), "AppFunctionId", "FunctionName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGroupPermissionDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var permission = new GroupPermission
            {
                UserGroupId = dto.UserGroupId,
                AppFunctionId = dto.AppFunctionId,
                CanView = dto.CanView,
                CanCreate = dto.CanCreate,
                CanEdit = dto.CanEdit,
                CanDelete = dto.CanDelete
            };

            _context.GroupPermissions.Add(permission);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ==============================
        // EDIT
        // ==============================
        public async Task<IActionResult> Edit(int id)
        {
            var permission = await _context.GroupPermissions.FindAsync(id);

            if (permission == null)
                return NotFound();

            var dto = new UpdateGroupPermissionDto
            {
                GroupPermissionId = permission.GroupPermissionId,
                UserGroupId = permission.UserGroupId,
                AppFunctionId = permission.AppFunctionId,
                CanView = permission.CanView,
                CanCreate = permission.CanCreate,
                CanEdit = permission.CanEdit,
                CanDelete = permission.CanDelete
            };

            ViewBag.Groups = new SelectList(await _context.UserGroups.ToListAsync(), "UserGroupId", "GroupName");
            ViewBag.Functions = new SelectList(await _context.AppFunctions.ToListAsync(), "AppFunctionId", "FunctionName");

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateGroupPermissionDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var permission = await _context.GroupPermissions.FindAsync(dto.GroupPermissionId);

            if (permission == null)
                return NotFound();

            permission.UserGroupId = dto.UserGroupId;
            permission.AppFunctionId = dto.AppFunctionId;
            permission.CanView = dto.CanView;
            permission.CanCreate = dto.CanCreate;
            permission.CanEdit = dto.CanEdit;
            permission.CanDelete = dto.CanDelete;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ==============================
        // DELETE
        // ==============================
        public async Task<IActionResult> Delete(int id)
        {
            var permission = await _context.GroupPermissions.FindAsync(id);

            if (permission == null)
                return NotFound();

            _context.GroupPermissions.Remove(permission);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}