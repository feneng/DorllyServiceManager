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
    public class RoleController : BaseController
    {
        private IRoleManager _roleManager;
        private DorllyServiceManagerContext _context;
        public RoleController(IUserManager userManager,IRoleManager roleManager,DorllyServiceManagerContext context) : base(userManager) {
            _roleManager = roleManager;
            _context = context;
        }
        // GET: /<controller>/
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = _roleManager.GetIndexQuery();
            return View(await list.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var role = new Role();
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Code", "Name",  "Status",  "Description")] Role role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _roleManager.AddEntityAsync(role);
                    await _roleManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "创建失败:" + ex.Message);
            }
            return View(role);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }
            var role = await _context.Role
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                .Include(r => r.UserRoles)
                    .ThenInclude(ur => ur.User)
                        .ThenInclude(u=>u.BelongGarden)
                //.AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
            if (role != null)
            {
                return View(role);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var roleToUpdate = await _roleManager.FindEntityAsync(id.Value);
            if (await TryUpdateModelAsync<Role>(roleToUpdate, "",
                p => p.Code, p => p.Name, p => p.Status, p => p.Description))
            {
                try
                {
                    await _roleManager.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "编辑权限信息失败");
                }
            }

            return View(roleToUpdate);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var role =await _context.Role
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp=>rp.Permission)
                .Include(r => r.UserRoles)
                    .ThenInclude(ur => ur.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }
    }
}
