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
    public class ContractController : BaseController
    {
        private DorllyServiceManagerContext _context;
        private IContractManager _contractManager;
        public ContractController(IUserManager userManager, IContractManager contractManager,IGardenManager gardenManager,
            DorllyServiceManagerContext context) : base(userManager,gardenManager)
        {
            _context = context;
            _contractManager = contractManager;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _contractManager.LoadEntityAllAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var contract = new Contract();
            ViewBag.Gardens = base.PopulateGardenDropDownList(null);
            return View(contract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Name", "ContractNo", "FirstParty", "SecondParty","BelongGardenId", "SupplierId",
                 "Content", "Enclosure", "SignDate", "State")] Contract contract)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _contractManager.AddEntityAsync(contract);
                    await _contractManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "创建失败:" + ex.Message);
            }
            ViewBag.Gardens = base.PopulateGardenDropDownList(contract.BelongGardenId);
            return View(contract);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var contract = await _context.Contract
                .SingleOrDefaultAsync(r => r.Id == id);

            if (contract != null)
            {
                ViewBag.Gardens = base.PopulateGardenDropDownList(contract.BelongGardenId);
                return View(contract);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var contractToUpdate = await _contractManager.FindEntityAsync(id.Value);
            if (await TryUpdateModelAsync(contractToUpdate, "",
                 c => c.Name, c => c.ContractNo, c => c.FirstParty,
                 c => c.SecondParty, c => c.BelongGardenId, c => c.SupplierId,
                 c => c.Content, c => c.Enclosure, c => c.SignDate, c => c.State))
            {
                try
                {
                    await _contractManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "编辑园区信息失败");
                }

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Gardens = base.PopulateGardenDropDownList(contractToUpdate.BelongGardenId);
            return View(contractToUpdate);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contract = await _context.Contract
                .SingleOrDefaultAsync(g => g.Id == id.Value);
            if (contract == null)
            {
                return NotFound();
            }
            return View(contract);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contract = await _context.Contract
                .SingleOrDefaultAsync(g => g.Id == id.Value);

            if (contract == null)
            {
                return NotFound();
            }
            return View(contract);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await _contractManager.DelEntityAsync(g => g.Id == id);
            //TODO:这里要级联删除以园区为外键的数据
            await _contractManager.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
