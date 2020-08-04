using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DorllyService.Domain;
using DorllyService.Service;
using Microsoft.AspNetCore.Mvc;

namespace DorllyServiceManager.Areas.Admin.Controllers
{
    public class SettlementController : BaseController
    {

        private DorllyServiceManagerContext _context;
        private IServiceOrderManager _serviceOrderManager;

        public SettlementController(IUserManager userManager,IServiceOrderManager serviceOrderManager, DorllyServiceManagerContext context)
            : base(userManager)
        {
            _serviceOrderManager = serviceOrderManager;
            _context = context;
        }
        // GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult OrderSettlement() {


            return View();
        }
    }
}
