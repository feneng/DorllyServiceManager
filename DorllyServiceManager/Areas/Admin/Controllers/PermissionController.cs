using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DorllyService.Common;
using DorllyService.Domain;
using DorllyService.IService;
using DorllyServiceManager.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DorllyServiceManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class PermissionController : BaseController
    {
        private readonly IPermissionManager _permissionManager;
        private readonly DorllyServiceManagerContext _context;
        public PermissionController(IUserManager userManager, IModuleManager moduleManager,
            IPermissionManager permissionManager, DorllyServiceManagerContext context
            ) : base(userManager, moduleManager: moduleManager)
        {
            _context = context;
            _permissionManager = permissionManager;
        }

        public async Task<IActionResult> Index(int? pageIndex = 1, int? pageSize = 10)
        {
            var pageInfo = new PageInfo<Permission>
            {
                length = pageSize.Value,
                pageIndex = pageIndex.Value
            };
            await _permissionManager.GetPageListAsync(p => p, p=>true, pageInfo,_context.Set<Permission>().Include(p=>p.BelongModule));
            return View(pageInfo);
        }

        [SkipAdminAuthorize]
        public async Task<IActionResult> Search(PageInfo<Permission> jsonData)
        {
            var list = await _permissionManager.GetPageListAsync(g => g, g => true, jsonData, _context.Set<Permission>().Include(p => p.BelongModule));
            jsonData.data = list;
            return Json(jsonData);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var permission = new Permission();
            ViewBag.Modules = base.PopulateModuleDropDownList();
            return View(permission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Code", "Name", "Type", "Status", "BelongModuleId", "Path", "Description", "Order", "Icon")] Permission permission)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _permissionManager.AddEntityAsync(permission);
                    await _permissionManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "创建失败:" + ex.Message);
            }
            return View(permission);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }
            var permission = await _permissionManager.FindEntityAsync(id.Value);
            if (permission != null)
            {
                ViewBag.Modules = base.PopulateModuleDropDownList();
                return View(permission);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var permissionToUpdate = await _permissionManager.FindEntityAsync(id.Value);
            if (await TryUpdateModelAsync<Permission>(permissionToUpdate, "", p => p.Code, p => p.Name, p => p.Type,
                p => p.BelongModuleId, p => p.Status, p => p.Description, p => p.Order, p => p.Icon, p => p.Path))
            {
                try
                {
                    await _permissionManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "编辑权限信息失败");
                }
            }

            ViewBag.Modules = base.PopulateModuleDropDownList();
            return View(permissionToUpdate);
        }

        [HttpGet, ActionName("EditRole")]
        public async Task<IActionResult> EditPermissionRole(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var permission = await _permissionManager.LoadEntityAsNoTrackingAsync(p => p.Id == id.Value);
            if (permission == null)
            {
                return NotFound();
            }

            var permissions = PopulateAssignedPermissionData(permission);
            return View(permissions);
        }

        private List<PermissionRoleData> PopulateAssignedPermissionData(Permission permission)
        {
            var allRoles = _context.Role;
            var roleIds = new HashSet<int>(permission.RolePermissions.Select(r => r.RoleId));
            var viewModel = new List<PermissionRoleData>();
            foreach (var item in allRoles)
            {
                viewModel.Add(new PermissionRoleData
                {
                    PermissionId = permission.Id,
                    RoleId = item.Id,
                    RoleCode = item.Code,
                    RoleName = item.Name,
                    Assigned = roleIds.Contains(item.Id),
                    Status = item.Status
                });
            }
            return viewModel;
        }

        [HttpPost, ActionName("EditRole")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPermissionRoleCallBack(int permissionId, string[] roleCheck)
        {
            var permission = await _permissionManager.LoadEntityAsNoTrackingAsync(p => p.Id == permissionId);
            if (permission == null)
            {
                return NotFound();
            }
            _context.RolePermission.RemoveRange(permission.RolePermissions);
            if (roleCheck.Length > 0)
            {
                foreach (var item in roleCheck)
                {
                    if (int.TryParse(item, out int result))
                    {
                        RolePermission entity = new RolePermission
                        {
                            PermissionId = permission.Id,
                            RoleId = result
                        };
                        await _context.RolePermission.AddAsync(entity);
                    }
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var permission = await _permissionManager.LoadEntityAsNoTrackingAsync(p => p.Id == id.Value);
            if (permission == null)
            {
                return NotFound();
            }
            return View(permission);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var permission = await _context.Permission.Include(p=>p.BelongModule)
                .SingleOrDefaultAsync(g => g.Id == id.Value);

            if (permission == null)
            {
                return NotFound();
            }

            ViewBag.Modules = base.PopulateModuleDropDownList(permission.BelongModuleId);
            return View(permission);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await _permissionManager.DelEntityAsync(g => g.Id == id);
            await _permissionManager.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
