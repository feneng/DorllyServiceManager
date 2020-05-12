using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DorllyService.Common;
using DorllyService.Domain;
using DorllyService.Service;
using DorllyServiceManager.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DorllyServiceManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class RoleController : BaseController
    {
        private IRoleManager _roleManager;
        private DorllyServiceManagerContext _context;

        public RoleController(IUserManager userManager,
            IRoleManager roleManager,
            DorllyServiceManagerContext context) : base(userManager)
        {
            _roleManager = roleManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = _roleManager.GetIndexQuery();
            return View(await list.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var role = new Role();
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Code", "Name", "Status", "Description")] Role role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _roleManager.AddEntityAsync(role);
                    await _roleManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "创建失败:" + ex.Message);
            }
            return View(role);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }
            var role = await _context.Role
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                .Include(r => r.UserRoles)
                    .ThenInclude(ur => ur.User)
                        .ThenInclude(u => u.BelongGarden)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (role != null)
            {
                ViewBag.Permissions = PopulateAssignedPermissionData(role);
                return View(role);
            }
            return NotFound();
        }

        private List<RolePermissionData> PopulateAssignedPermissionData(Role role)
        {
            var allPerssions = _context.Permission;
            var rolePermissions = new HashSet<int>(role.RolePermissions.Select(p => p.PermissionId));
            var viewModel = new List<RolePermissionData>();
            foreach (var item in allPerssions)
            {
                viewModel.Add(new RolePermissionData
                {
                    RoleId = role.Id,
                    PermissionId = item.Id,
                    PermissionName = item.Name,
                    PermissionCode = item.Code,
                    PermissionIcon = item.Icon,
                    PermissionOrder = item.Order,
                    Assigned = rolePermissions.Contains(item.Id),
                    Status = item.Status
                });
            }
            return viewModel;
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var roleToUpdate = await _roleManager.FindEntityAsync(id.Value);
            if (await TryUpdateModelAsync(roleToUpdate, "",
                p => p.Code, p => p.Name, p => p.Status, p => p.Description))
            {
                try
                {
                    await _roleManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "编辑权限信息失败");
                }
            }

            return View(roleToUpdate);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var role = await _context.Role
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                .Include(r => r.UserRoles)
                    .ThenInclude(ur => ur.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        [HttpGet, ActionName("EditPermission")]
        public async Task<IActionResult> EditRolePermission(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var role = await _context.Role
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            var permissions = PopulateAssignedPermissionData(role);
            return View(permissions);
        }

        [HttpPost, ActionName("EditPermission")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRolePermissionCallBack(int roleId, string[] permissionCheck)
        {
            var role = await _context.Role
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == roleId);

            _context.RolePermission.RemoveRange(role.RolePermissions);

            if (permissionCheck.Length > 0)
            {
                foreach (var item in permissionCheck)
                {
                    if (int.TryParse(item, out int result))
                    {
                        RolePermission entity = new RolePermission
                        {
                            RoleId = role.Id,
                            PermissionId = result
                        };
                        await _context.RolePermission.AddAsync(entity);
                    }
                }
            }
            await _context.SaveChangesAsync();

            if (role == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var role = await _context.Role
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                .Include(r => r.UserRoles)
                    .ThenInclude(ur => ur.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            ViewBag.Permissions = PopulateAssignedPermissionData(role);
            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await _roleManager.DelEntityAsync(r => r.Id == id);
            await _roleManager.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
