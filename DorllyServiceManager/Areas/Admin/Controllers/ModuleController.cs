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
    public class ModuleController : BaseController
    {
        private IModuleManager _moduleManager;
        private DorllyServiceManagerContext _context;

        public ModuleController(DorllyServiceManagerContext context,
            IModuleManager moduleManager,IUserManager userManager,ISubSystemManager subSystemManager)
            :base(userManager,subSystemManager:subSystemManager)
        {
            _context = context;
            _moduleManager = moduleManager;
        }

        public IActionResult Index()
        {
            var treeList = _moduleManager.GetModuleTree();
            return View(treeList);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            var module = new Module();
            if (id.HasValue)
            {
                var parentModule =await _moduleManager.FindEntityAsync(id.Value);
                if (parentModule != null)
                {
                    module.Level = parentModule.Level + 1;
                    module.ParentId = parentModule.Id;
                    module.Parent = parentModule;
                    module.BelongSystemId = parentModule.BelongSystemId;
                }
            }
            else
            {
                module.Level = 1;
                module.ParentId = null;
            }

            ViewBag.SubSystems = base.PopulateSubSystemDropDownList(module.BelongSystemId);
            return View(module);
        }

        [HttpPost,ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(
            [Bind("Name","Type","Icon","ParentId", "Status", "BelongSystemId","Level","Order","Path")] Module module)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _moduleManager.AddEntityAsync(module);
                    await _moduleManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "创建失败:"+ex.Message);
            }

            ViewBag.SubSystems = base.PopulateSubSystemDropDownList(module.BelongSystemId);
            return View(module);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var module = await _context.Module
                .Include(m=>m.Parent)
                .AsNoTracking()
                .SingleOrDefaultAsync(m=>m.Id==id.Value);
            if (module!=null)
            {
                ViewBag.SubSystems = base.PopulateSubSystemDropDownList(module.BelongSystemId);
                return View(module);
            }
               
            return NotFound();
        }

        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var moduleToUpdate = await _moduleManager.FindEntityAsync(id.Value);
            if (await TryUpdateModelAsync<Module>(moduleToUpdate, "", m => m.Name, m => m.Type,m=>m.Icon,
                m=>m.BelongSystemId,m=>m.Status,m=>m.ParentId,m=>m.Order, m => m.Level, m => m.Path))
            {
                try
                {
                    await _moduleManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "编辑菜单信息失败");
                }
            }

            ViewBag.SubSystems = base.PopulateSubSystemDropDownList(moduleToUpdate.BelongSystemId);
            return View(moduleToUpdate);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var module = await _moduleManager.FindEntityAsync(id.Value);
            if (module == null)
            {
                return NotFound();
            }
            ViewBag.SubSystems = base.PopulateSubSystemDropDownList(module.BelongSystemId);
            return View(module);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await _moduleManager.DelEntityAsync(m=>m.Id==id);
            await _moduleManager.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
