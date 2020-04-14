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
    public class SubSystemController : BaseController
    {
        private DorllyServiceManagerContext _context;
        private ISubSystemManager _subSystemManager;
        public SubSystemController(IUserManager userManager,ISubSystemManager subSystemManager,
            DorllyServiceManagerContext context) : base(userManager)
        {
            _context = context;
            _subSystemManager = subSystemManager;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _subSystemManager.LoadEntityAllAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var system = new SubSystem();
            return View(system);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind( "Name", "Code", "Description", "State")] SubSystem system)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _subSystemManager.AddEntityAsync(system);
                    await _subSystemManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "创建失败:" + ex.Message);
            }
            return View(system);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }
            var system = await _context.SubSystem
                .SingleOrDefaultAsync(r => r.Id == id);
            if (system != null)
            {
                return View(system);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var systemToUpdate = await _subSystemManager.FindEntityAsync(id.Value);
            if (await TryUpdateModelAsync(systemToUpdate, "",
                 s => s.Name, s => s.Code, s => s.Description,  u => u.State))
            {
                try
                {
                    await _subSystemManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "编辑用户信息失败");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(systemToUpdate);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var system = await _context.SubSystem
                .SingleOrDefaultAsync(r => r.Id == id);
            if (system == null)
            {
                return NotFound();
            }
            return View(system);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var system = await _context.SubSystem
                .SingleOrDefaultAsync(r => r.Id == id);
            if (system == null)
            {
                return NotFound();
            }
            return View(system);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            //TODO:待修正，处理关联数据
            var result = await _subSystemManager.DelEntityAsync(s=>s.Id==id);
            await _subSystemManager.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
