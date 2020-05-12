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
    public class ServicePropertyController : BaseController
    {
        private IServicePropertyManager _servicePropertyManager;
        private DorllyServiceManagerContext _context;

        public ServicePropertyController(IUserManager userManager,
            IServicePropertyManager servicePropertyManager,
            IServiceCategoryManager serviceCategoryManager,
            DorllyServiceManagerContext context)
            : base(userManager, serviceCategoryManager: serviceCategoryManager)
        {
            _servicePropertyManager = servicePropertyManager;
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var list = _servicePropertyManager.GetIndexQuery();
            return View(await list.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var property = new ServiceProperty();
            //ViewBag.Categories = base.PopulateServiceCategoryDropDownList(null);
            return View(property);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Code", "Name", "State", "Type", "InputType", "NotNull")] ServiceProperty property)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _servicePropertyManager.AddEntityAsync(property);
                    await _servicePropertyManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "创建失败:" + ex.Message);
            }
            return View(property);
        }
    }
}
