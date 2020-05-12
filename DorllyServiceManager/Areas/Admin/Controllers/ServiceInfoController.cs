using System;
using System.Threading.Tasks;
using DorllyService.Domain;
using DorllyService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DorllyServiceManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class ServiceInfoController : BaseController
    {
        private IServiceManager _serviceManager;
        private DorllyServiceManagerContext _context;

        public ServiceInfoController(IUserManager userManager,
            IServiceManager serviceManager,
            DorllyServiceManagerContext context) : base(userManager)
        {
            _serviceManager = serviceManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = _serviceManager.GetIndexQuery();
            return View(await list.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var service = new Service();
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Code", "Name", "State", "Description")] Service service)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _serviceManager.AddEntityAsync(service);
                    await _serviceManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "创建失败:" + ex.Message);
            }
            return View(service);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }
            var service = await _context.Service
                .Include(s => s.Category)
                    .ThenInclude(c => c.Parent)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (service != null)
            {
                return View(service);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var serviceToUpdate = await _serviceManager.FindEntityAsync(id.Value);
            if (await TryUpdateModelAsync(serviceToUpdate, "",
                s => s.Name, s => s.Code, s => s.State, s => s.Description,s=>s.CategoryId))
            {
                try
                {
                    await _serviceManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "编辑服务信息失败");
                }
            }

            return View(serviceToUpdate);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Service
                 .Include(s => s.Category)
                     .ThenInclude(c => c.Parent)
                 .AsNoTracking()
                 .FirstOrDefaultAsync(r => r.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Service
                 .Include(s => s.Category)
                     .ThenInclude(c => c.Parent)
                 .AsNoTracking()
                 .FirstOrDefaultAsync(r => r.Id == id);

            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await _serviceManager.DelEntityAsync(r => r.Id == id);
            await _serviceManager.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
