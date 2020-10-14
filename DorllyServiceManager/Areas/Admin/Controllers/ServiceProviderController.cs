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
    public class ServiceProviderController : BaseController
    {
        private IServiceProviderManager _serviceProviderManager;
        private DorllyServiceManagerContext _context;

        public ServiceProviderController(IUserManager userManager,IGardenManager gardenManager,
            IServiceProviderManager serviceProviderManager,DorllyServiceManagerContext context)
            : base(userManager,gardenManager) {
            _serviceProviderManager = serviceProviderManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync(int? pageIndex = 1, int? pageSize = 10)
        {
            var pageInfo = new PageInfo<ServiceSupplier>
            {
                length = pageSize.Value,
                pageIndex = pageIndex.Value
            };

            pageInfo.data = await _serviceProviderManager.GetPageListAsync(sp => sp, sp => true, pageInfo,
                _context.Set<ServiceSupplier>()
                    .Include(sp => sp.BelongGarden));
            return View(pageInfo);
        }

        [SkipAdminAuthorize]
        public async Task<IActionResult> Search(PageInfo<ServiceSupplier> jsonData)
        {
            jsonData.data = await _serviceProviderManager.GetPageListAsync(sp => sp, sp => true, jsonData,
                _context.Set<ServiceSupplier>()
                    .Include(sp => sp.BelongGarden));
            return Json(jsonData);
        }

        [HttpGet]
        public async Task<IActionResult> Apply()
        {
            var query =await _serviceProviderManager.LoadEntityAsNoTrackingAsync(s=>s.ApproveState==1);
            return View(query);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var provider = new ServiceSupplier();
            ViewBag.Gardens = base.PopulateGardenDropDownList(null);
            return View(provider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Code", "FullName", "Abbreviation", "BelongGardenId",
            "ServiceScope","ChargePerson","ContactPhone","Email",
            "WorkTel", "SupplierFrom","Avatar")] ServiceSupplier supplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _serviceProviderManager.AddEntityAsync(supplier);
                    await _serviceProviderManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "创建失败:" + ex.InnerException.Message);
            }
            return View(supplier);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }
            var supplier = await _context.ServiceSupplier
                .Include(s => s.BelongGarden)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (supplier != null)
            {
                ViewBag.Gardens = base.PopulateGardenDropDownList(supplier.BelongGarden);
                return View(supplier);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var supplierToUpdate = await _serviceProviderManager.FindEntityAsync(id.Value);
            if (await TryUpdateModelAsync(supplierToUpdate, string.Empty,
                 s=> s.Code, s =>s.FullName, s => s.Abbreviation, s => s.WorkTel,s=>s.SupplierFrom,s=>s.BelongGardenId,
                 s=>s.Avatar,s=>s.Email,s=>s.ContactPhone,s=>s.ChargePerson,s=>s.ServiceScope ))
            {
                try
                {
                    await _serviceProviderManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "编辑服务商信息失败");
                }
            }

            ViewBag.Gardens = base.PopulateGardenDropDownList(supplierToUpdate.BelongGarden);
            return View(supplierToUpdate);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supplier = await _context.ServiceSupplier
               .Include(s => s.BelongGarden)
               .AsNoTracking()
               .FirstOrDefaultAsync(r => r.Id == id);

            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supplier = await _context.ServiceSupplier
               .Include(s => s.BelongGarden)
               .AsNoTracking()
               .FirstOrDefaultAsync(r => r.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            ViewBag.Gardens = base.PopulateGardenDropDownList(supplier.BelongGarden);
            return View(supplier);
        }

        public async Task<IActionResult> Approve(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }
            var supplier = await _serviceProviderManager.FindEntityAsync(id.Value);
            if (supplier==null)
            {
                return NotFound();
            }
            supplier.ApproveState = 1;
            await _serviceProviderManager.CommitAsync();
            return Json(new { state = true });
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await _serviceProviderManager.DelEntityAsync(r => r.Id == id);
            await _serviceProviderManager.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
