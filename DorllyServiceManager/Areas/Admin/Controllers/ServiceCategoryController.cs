using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DorllyService.Domain;
using DorllyService.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DorllyServiceManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class ServiceCategoryController : BaseController
    {
        private IServiceCategoryManager _serviceCategoryManager;
        private DorllyServiceManagerContext _context;

        public ServiceCategoryController(IServiceCategoryManager serviceCategoryManager,
            DorllyServiceManagerContext context,
            IUserManager userManager) : base(userManager)
        {
            _serviceCategoryManager = serviceCategoryManager;
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var treeList = _serviceCategoryManager.GetServiceCategoryTree();
            return View(treeList);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            var category = new ServiceCategory();
            if (id.HasValue)
            {
                var parentCategory = await _serviceCategoryManager.FindEntityAsync(id.Value);
                if (parentCategory != null)
                {
                    category.Level = parentCategory.Level + 1;
                    category.ParentId = parentCategory.Id;
                    category.Parent = parentCategory;
                }
            }
            else
            {
                category.Level = 1;
                category.ParentId = null;
            }

            //ViewBag.SubSystems = base.PopulateSubSystemDropDownList(module.BelongSystemId);
            return View(category);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(
            [Bind("Name", "Code", "ParentId", "Status",  "Level", "Remark", "Status")] ServiceCategory category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _serviceCategoryManager.AddEntityAsync(category);
                    await _serviceCategoryManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "创建失败:" + ex.Message);
            }

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var category = await _context.ServiceCategory
                .Include(m => m.Parent)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id.Value);
            if (category != null)
            {
                //ViewBag.SubSystems = base.PopulateSubSystemDropDownList(module.BelongSystemId);
                return View(category);
            }

            return NotFound();
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var categoryToUpdate = await _serviceCategoryManager.FindEntityAsync(id.Value);
            if (await TryUpdateModelAsync<ServiceCategory>(categoryToUpdate, "", m => m.Name, m => m.Code,
                m => m.ParentId, m => m.Status,  m => m.Status, m => m.Level, m => m.Remark))
            {
                try
                {
                    await _serviceCategoryManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "编辑菜单信息失败");
                }
            }

            //ViewBag.SubSystems = base.PopulateSubSystemDropDownList(moduleToUpdate.BelongSystemId);
            return View(categoryToUpdate);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _serviceCategoryManager.FindEntityAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }
            //ViewBag.SubSystems = base.PopulateSubSystemDropDownList(module.BelongSystemId);
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await _serviceCategoryManager.DelEntityAsync(m => m.Id == id);
            await _serviceCategoryManager.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
