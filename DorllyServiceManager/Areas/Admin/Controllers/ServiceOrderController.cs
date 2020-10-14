using System.Threading.Tasks;
using DorllyService.Domain;
using DorllyService.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DorllyServiceManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class ServiceOrderController : BaseController
    {
        private IServiceOrderManager _serviceOrderManager;
        private DorllyServiceManagerContext _context;

        public ServiceOrderController(IServiceOrderManager serviceOrderManager,
            DorllyServiceManagerContext context,
            IUserManager userManager) : base(userManager)
        {
            _serviceOrderManager = serviceOrderManager;
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var list = _serviceOrderManager.LoadEntityEnumerable(o => o.State == 1, o => o.OrderNo, "desc", 1, 10);
            return View(list);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = await _context.Order
                .Include(o => o.BelongGarden)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
    }
}
