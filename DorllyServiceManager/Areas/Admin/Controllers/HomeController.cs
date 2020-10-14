using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DorllyService.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        public IActionResult Profile()
        {
            ViewBag.UserName = CurrentUser.Name;
            ViewBag.UserDescription = CurrentUser.Email;
            return View();
        }
    }
}
