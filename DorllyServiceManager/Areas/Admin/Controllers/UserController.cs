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
    public class UserController : BaseController
    {
        private DorllyServiceManagerContext _context;
        private IUserManager _userManager;
        public UserController(IUserManager userManager, IGardenManager gardenManager,
            DorllyServiceManagerContext context) : base(userManager, gardenManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var list = _userManager.GetIndexQuery();
            return View(await list.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var role = new User();
            ViewBag.Gardens = base.PopulateGardenDropDownList();
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Account", "Name", "Email", "Sex", "ContactPhone", "BelongGardenId", "State")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.CreateDate = DateTime.UtcNow;
                    await _userManager.AddEntityAsync(user);
                    await _userManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "创建失败:" + ex.Message);
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }
            var user = await _context.User
                .Include(u => u.BelongGarden)
                .Include(r => r.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
            ViewBag.Gardens = base.PopulateGardenDropDownList(user.BelongGardenId);
            if (user != null)
            {
                return View(user);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var userToUpdate = await _userManager.FindEntityAsync(id.Value);
            if (await TryUpdateModelAsync(userToUpdate, "",
                u => u.Account, u => u.Name, u => u.Password, u => u.Email, u => u.Sex, u => u.ContactPhone, u => u.BelongGardenId, u => u.Avatar, u => u.State))
            {
                try
                {
                    await _userManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "编辑用户信息失败");
                }

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Gardens = base.PopulateGardenDropDownList(userToUpdate.BelongGardenId);
            return View(userToUpdate);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _context.User
                .Include(u => u.BelongGarden)
                .Include(r => r.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _context.User
                .Include(u => u.BelongGarden)
                .Include(r => r.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id) {
            var result= await _userManager.Remove(id);
            await _userManager.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
