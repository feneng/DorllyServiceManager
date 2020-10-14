using System;
using System.Linq;
using System.Threading.Tasks;
using DorllyService.Common;
using DorllyService.Domain;
using DorllyService.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DorllyServiceManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class GardenController : BaseController
    {
        private DorllyServiceManagerContext _context;
        private IGardenManager _gardenManager;
        public GardenController(IUserManager userManager, IGardenManager gardenManager,
            DorllyServiceManagerContext context) : base(userManager, gardenManager)
        {
            _context = context;
            _gardenManager = gardenManager;
        }

        public async Task<IActionResult> Index(int? pageIndex = 1,int? pageSize=10)
        {
            var count = await _gardenManager.GetEntitiesCountAsync(g=>true);
            var list = await _gardenManager.LoadEntityListAsync(g=>true, g => g.Id, "asc", pageIndex.Value, pageSize.Value);
            var pageInfo = new PageInfo<Garden>(0,pageIndex.Value, pageSize.Value, count,list.Count(),list);
            return View(pageInfo);
        }

        [SkipAdminAuthorize]
        public async Task<IActionResult> Search(PageInfo<Garden> jsonData)
        {
            var list =await _gardenManager.GetPageListAsync(g => g, g => true, jsonData);
            jsonData.data = list;
            return Json(jsonData);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var garden = new Garden();
            return View(garden);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Name", "Address", "State")] Garden garden)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _gardenManager.AddEntityAsync(garden);
                    await _gardenManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "创建失败:" + ex.Message);
            }
            return View(garden);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var garden = await _context.Garden
                .SingleOrDefaultAsync(r => r.Id == id);

            if (garden != null)
            {
                return View(garden);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var gardenToUpdate = await _gardenManager.FindEntityAsync(id.Value);
            if (await TryUpdateModelAsync(gardenToUpdate, "",
                 g => g.Name, g => g.Address, g => g.State))
            {
                try
                {
                    await _gardenManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "编辑园区信息失败");
                }

                return RedirectToAction(nameof(Index));
            }

            return View(gardenToUpdate);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var garden = await _context.Garden
                .SingleOrDefaultAsync(g => g.Id == id.Value);
            if (garden == null)
            {
                return NotFound();
            }
            return View(garden);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var garden = await _context.Garden
                .SingleOrDefaultAsync(g => g.Id == id.Value);

            if (garden == null)
            {
                return NotFound();
            }
            return View(garden);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await _gardenManager.DelEntityAsync(g => g.Id == id);
            await _gardenManager.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
