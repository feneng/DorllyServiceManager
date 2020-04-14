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
        // GET: /<controller>/
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var query = await _serviceProviderManager.LoadEntityAllAsync();
            return View(query);
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
                ModelState.AddModelError("", "创建失败:" + ex.Message);
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
            if (await TryUpdateModelAsync(supplierToUpdate, "",
                 s=> s.Code, s =>s.FullName, s => s.Abbreviation, s => s.WorkTel))
            {
                try
                {
                    await _serviceProviderManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "编辑权限信息失败");
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
