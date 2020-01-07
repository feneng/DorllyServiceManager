using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DorllyService.Domain;
using DorllyService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DorllyServiceManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class PermissionController : BaseController
    {
        private readonly IPermissionManager _permissionManager;
        public PermissionController(IUserManager userManager,
            IPermissionManager permissionManager
            ):base(userManager) {
            _permissionManager = permissionManager;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var list=await _permissionManager.LoadEntityAllAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var permission = new Permission();

            return View(permission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Code","Name","Type","Status", "BelongModuleId","Path", "Description","Order","Icon")] Permission permission)
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
            if (permission!=null)
            {
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
            if (await TryUpdateModelAsync<Permission>(permissionToUpdate, "", p=>p.Code,p => p.Name, p => p.Type,
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

            return View(permissionToUpdate);
        }

    }
}
