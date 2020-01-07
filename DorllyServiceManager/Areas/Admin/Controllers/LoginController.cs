using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DorllyService.Common;
using DorllyService.Domain;
using DorllyService.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DorllyServiceManager.Areas.Admin.Controllers
{
    [Area(areaName: "Admin")]
    public class LoginController : Controller
    {
        private readonly DorllyServiceManagerContext _context;
        private readonly IUserManager _userManage;
        public LoginController(DorllyServiceManagerContext context, IUserManager userManage)
        {
            _context = context;
            _userManage = userManage;
        }

        // GET: /<controller>/
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var adminCookie = await HttpContext.AuthenticateAsync(AdminAuthorizeAttribute.AdminAuthenticationScheme);
            var principal = adminCookie.Principal;
            if (principal != null)
            {
                var account = principal.Claims.FirstOrDefault(x => x.Type == "Account")?.Value;
                var password = principal.Claims.FirstOrDefault(x => x.Type == "Password")?.Value;
                if (!string.IsNullOrEmpty(account))
                {
                    var salt = _userManage.GetUserSalt(account);
                    password = DesEncrypt.Decrypt(password, salt);
                    var user = _userManage.Login(account, password);
                    if (user != null)
                    {
                        HttpContext.Session.Set(AdminAuthorizeAttribute.AdminAuthenticationScheme, ByteConvertHelper.Object2Bytes(user));
                        var jumpPage = JsonConfigurationHelper.GetAppSettings<SiteSetting>("appsettings.json", "SiteConfig")?.JumpPage;
                        return new RedirectResult(string.IsNullOrEmpty(jumpPage) ? "~/Admin/Home/Index" : jumpPage);
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string account, string password, string rememberMe, string code)
        {
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                TempData["SessionLoseError"] = "用户名和密码不能为空!";
                return RedirectToAction(nameof(Index));
            }
            var currentUser = _userManage.Login(account, password);
            if (currentUser == null)
            {
                TempData["SessionLoseError"] = "用户名或密码不正确!";
                return RedirectToAction(nameof(Index));
            }
            if (currentUser.State == 0)
            {
                TempData["SessionLoseError"] = "用户已禁用，请联系管理员!";
                return RedirectToAction(nameof(Index));
            }

            HttpContext.Session.Set(AdminAuthorizeAttribute.AdminAuthenticationScheme, ByteConvertHelper.Object2Bytes(currentUser));
            var ssoServer = JsonConfigurationHelper.GetAppSettings<SiteSetting>("appsettings.json", "SiteConfig")?.SsoServer;
            if (string.IsNullOrEmpty(ssoServer))
            {
                var claims = new[] { new Claim("Account", currentUser.UserAccount), new Claim("Password", currentUser.Password) };
                var ssoUser = new ClaimsPrincipal(new ClaimsIdentity(claims, AdminAuthorizeAttribute.AdminAuthenticationScheme));
                await HttpContext.SignInAsync(AdminAuthorizeAttribute.AdminAuthenticationScheme, ssoUser, new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromHours(1)),
                    IsPersistent = true,
                    AllowRefresh = false
                });

                var retUrl = Request.Query["returnUrl"];
                if (!string.IsNullOrEmpty(retUrl))
                {
                    return new RedirectResult(retUrl);
                }

                var jumpPage = JsonConfigurationHelper.GetAppSettings<SiteSetting>("appsettings.json", "SiteConfig")?.JumpPage;
                return new RedirectResult(string.IsNullOrEmpty(jumpPage) ? "~/Admin/Home/Index" : jumpPage);
            }
            return new RedirectResult("~/SSO/RegisterSite");//跳转到集团注册页面
        }
    }
}
