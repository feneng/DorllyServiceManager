using System;
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
    public class ServiceInfoController : BaseController
    {
        private IServiceManager _serviceManager;
        private DorllyServiceManagerContext _context;

        public ServiceInfoController(IUserManager userManager,
            IServiceManager serviceManager,
            IServiceCategoryManager serviceCategoryManager,
            DorllyServiceManagerContext context) : base(userManager,serviceCategoryManager:serviceCategoryManager)
        {
            _serviceManager = serviceManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? pageIndex=1,int? pageSize=10)
        {
            var pageInfo = new PageInfo<Service>
            {
                length = pageSize.Value,
                pageIndex = pageIndex.Value
            };

            pageInfo.data = await _serviceManager.GetPageListAsync(p => p, p => true, pageInfo,
                _context.Set<Service>()
                .Include(s => s.Category)
                    .ThenInclude(c=>c.Parent)
                        .ThenInclude(c=>c.Parent)
                .Include(s=>s.UpdateUser));
            
            return View(pageInfo);
        }

        [SkipAdminAuthorize]
        public async Task<IActionResult> Search(PageInfo<Service> jsonData)
        {
            var list = await _serviceManager.GetPageListAsync(s => s, s => true, jsonData,
                _context.Set<Service>()
                .Include(s => s.Category)
                    .ThenInclude(c => c.Parent)
                        .ThenInclude(c => c.Parent)
                .Include(s => s.UpdateUser));
            jsonData.data = list;
            return Json(jsonData);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var service = new Service();
            ViewBag.ServiceCategories = PopulateServiceCategoriesDropDownList(null);
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Code", "Name", "State", "Description", "CategoryId")] Service service)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.UpdateDate = DateTime.Now;
                    service.UpdateUserId = base.CurrentUser.Id;
                    await _serviceManager.AddEntityAsync(service);
                    await _serviceManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "创建失败:" + ex.InnerException.Message);
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
                ViewBag.ServiceCategories = PopulateServiceCategoriesDropDownList(service.CategoryId);
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
                    serviceToUpdate.UpdateDate = DateTime.Now;
                    serviceToUpdate.UpdateUserId = base.CurrentUser.Id;
                    await _serviceManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "编辑服务信息失败:"+ex.InnerException.Message);
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
                 .Include(s=>s.UpdateUser)
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
