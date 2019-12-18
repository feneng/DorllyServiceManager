using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DorllyService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DorllyServiceManager.Areas.Admin.Controllers
{
    [Area(areaName:"Admin")]
    public class HomeController : BaseController
    {
        private readonly IUserManager _userManage;
        private readonly ILogger<HomeController> _logger;
        public HomeController(IUserManager userManage, ILogger<HomeController> logger) :base(userManage)
        {
            _userManage = userManage;
            _logger = logger;
        }
        // GET: /<controller>/
        [AdminAuthorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
