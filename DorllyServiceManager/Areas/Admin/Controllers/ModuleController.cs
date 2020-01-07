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
    public class ModuleController : Controller
    {
        private IModuleManager _moduleManager;
        public ModuleController(IModuleManager moduleManager)
        {
            _moduleManager = moduleManager;
        }

        public IActionResult Index()
        {
            var treeList = _moduleManager.GetModuleTree();
            return View(treeList);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? pid)
        {
            var module = new Module();
            if (pid.HasValue)
            {
                var parentModule =await _moduleManager.FindEntityAsync(pid.Value);
                if (parentModule != null)
                {
                    module.Level = parentModule.Level + 1;
                    module.ParentId = parentModule.Id;
                    module.BelongSystemId = parentModule.BelongSystemId;
                }
            }
            else
            {
                module.Level = 1;
                module.ParentId = null;
                module.BelongSystemId = 2;
            }
            
            module.Status = true;
            return View(module);
        }

        [HttpPost,ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(
            [Bind("Name","Type","ParentId", "Status", "BelongSystemId","Level","Order","Path")] Module module)
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

            return View(module);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id.HasValue)
            {
                var module= await  _moduleManager.FindEntityAsync(id.Value);
                return View(module);
            }
            return View(new Module());
        }

        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var moduleToUpdate = await _moduleManager.FindEntityAsync(id.Value);
            if (await TryUpdateModelAsync<Module>(moduleToUpdate, "", m => m.Name, m => m.Type,
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

            return View(moduleToUpdate);
        }

    }
}
