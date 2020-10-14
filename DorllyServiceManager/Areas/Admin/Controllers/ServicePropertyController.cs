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
    public class ServicePropertyController : BaseController
    {
        private IServicePropertyManager _servicePropertyManager;
        private IServiceCategoryManager _serviceCategoryManager;
        private IPropertyValueManager _propertyValueManager;
        private DorllyServiceManagerContext _context;

        public ServicePropertyController(IUserManager userManager,
            IServicePropertyManager servicePropertyManager,
            IServiceCategoryManager serviceCategoryManager,
            IPropertyValueManager propertyValueManager,
            DorllyServiceManagerContext context)
            : base(userManager, serviceCategoryManager: serviceCategoryManager)
        {
            _servicePropertyManager = servicePropertyManager;
            _serviceCategoryManager = serviceCategoryManager;
            _propertyValueManager = propertyValueManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync(int? pageIndex=1,int? pageSize=10)
        {
            var pageInfo = new PageInfo<ServiceProperty>
            {
                length = pageSize.Value,
                pageIndex = pageIndex.Value
            };

            pageInfo.data = await _servicePropertyManager.GetPageListAsync(sp => sp, sp => true, pageInfo,
                _context.Set<ServiceProperty>()
                    .Include(sp=>sp.Categories)
                        .ThenInclude(cp=>cp.Category)
                            .ThenInclude(c=>c.Parent)
                                .ThenInclude(c=>c.Parent));
            ViewBag.Categories =await base.GetServiceCategoriesForSelect2(_context.Set<ServiceCategory>().Include(sc=>sc.Parent).Include(sc=>sc.Children)).ToListAsync();
            return View(pageInfo);
        }

        [SkipAdminAuthorize]
        public async Task<IActionResult> Search(PageInfo<ServiceProperty> jsonData)
        {
            jsonData.data = await _servicePropertyManager.GetPageListAsync(sp => sp, sp => true, jsonData,
                 _context.Set<ServiceProperty>()
                    .Include(sp => sp.Categories)
                        .ThenInclude(cp => cp.Category)
                            .ThenInclude(c => c.Parent)
                                .ThenInclude(c => c.Parent));
            return Json(jsonData);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var property = new ServiceProperty();
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

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }
            var property = await _servicePropertyManager.LoadEntityAsNoTrackingAsync(sp => sp.Id == id.Value);

            if (property != null)
            {
                return View(property);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var propertyToUpdate = await _servicePropertyManager.LoadEntityAsync(sp => sp.Id == id.Value);
            if (await TryUpdateModelAsync(propertyToUpdate, "",
                s => s.Name, s => s.Code, s => s.State,s=>s.InputType,s=>s.NotNull,s=>s.Type))
            {
                try
                {
                    await _servicePropertyManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "编辑服务属性信息失败:" + ex.InnerException.Message);
                }
            }

            return View(propertyToUpdate);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var property = await _servicePropertyManager.LoadEntityAsNoTrackingAsync(sp => sp.Id == id.Value);

            if (property == null)
            {
                return NotFound();
            }

            return View(property);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var property = await _servicePropertyManager.LoadEntityAsNoTrackingAsync(sp => sp.Id == id.Value);

            if (property == null)
            {
                return NotFound();
            }

            return View(property);
        }

        public async Task<IActionResult> Deactivate(int id)
        {
            var property = await _servicePropertyManager.FindEntityAsync(id);
            if (property == null)
            {
                return NotFound();
            }
            property.State = (byte)(property.State==0?1:0);
            await _servicePropertyManager.CommitAsync();
            return Json(new { state = true });
        }

        [HttpPost]
        public async Task<IActionResult> EditPropertyCategory(int propertyId,int[] tags)
        {
            var property = await _servicePropertyManager.LoadEntityAsync(sp=>sp.Id==propertyId);
            _context.CategoryProperties.RemoveRange(property.Categories);
            if (tags.Length != 0)
            {
                foreach (var item in tags)
                {
                    if (await _serviceCategoryManager.AnyEntityAsync(sc => sc.Id == item))
                    {
                        var entity = new CategoryProperties {
                            CategoryId=item,
                            PropertyId = propertyId,
                        };
                       await _context.CategoryProperties.AddAsync(entity);
                    }
                }
            }
            await _servicePropertyManager.CommitAsync();
            return Json(new { state = true });
        }

        [HttpPost]
        public async Task<IActionResult> AllocatePropertyValue(PageInfo<PropertyValue> pageData)
        {
            var b = int.TryParse(pageData.search, out int result);
            if (!b)
            {
                return Json(pageData);
            }
            pageData.data = await _propertyValueManager.LoadEntityListAsNoTrackingAsync(pv => pv.PropertyId == result);
            return Json(pageData);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await _servicePropertyManager.DelEntityAsync(r => r.Id == id);
            await _servicePropertyManager.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}