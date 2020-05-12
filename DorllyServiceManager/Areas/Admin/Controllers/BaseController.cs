using System.Linq;
using DorllyService.Common;
using DorllyService.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace DorllyServiceManager.Areas.Admin.Controllers
{

    public class BaseController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IGardenManager _gardenManager;
        private readonly IModuleManager _moduleManager;
        private readonly ISubSystemManager _subSystemManager;
        private readonly IServiceCategoryManager _serviceCategoryManager;

        public BaseController(IUserManager userManager,
            IGardenManager gardenManager=null,
            IModuleManager moduleManager=null,
            ISubSystemManager subSystemManager=null,
            IServiceCategoryManager serviceCategoryManager=null
            )
        {
            _userManager = userManager;
            _gardenManager = gardenManager;
            _moduleManager = moduleManager;
            _subSystemManager = subSystemManager;
            _serviceCategoryManager = serviceCategoryManager;
        }

        public Account CurrentUser
        {
            get
            {
                try
                {
                    //从Session中获取用户对象
                    if (HttpContext.Session.TryGetValue(AdminAuthorizeAttribute.AdminAuthenticationScheme, out byte[] accountData))
                        return ByteConvertHelper.Bytes2Object<Account>(accountData);
                    //Session过期 通过Cookies中的信息 重新获取用户对象 并存储于Session中
                    var adminCookie = HttpContext.AuthenticateAsync(AdminAuthorizeAttribute.AdminAuthenticationScheme);
                    adminCookie.Wait();
                    var principal = adminCookie.Result.Principal;
                    if (principal != null)
                    {
                        var account = principal.Claims.FirstOrDefault(x => x.Type == "Account")?.Value;
                        var password = principal.Claims.FirstOrDefault(x => x.Type == "Password")?.Value;
                        if (!string.IsNullOrEmpty(account))
                        {
                            var salt = _userManager.GetUserSalt(account);
                            password = DesEncrypt.Decrypt(password, salt);
                            var user = _userManager.Login(account, password);
                            if (user != null)
                            {
                                HttpContext.Session.Set(AdminAuthorizeAttribute.AdminAuthenticationScheme, ByteConvertHelper.Object2Bytes(user));
                                return user;
                            }
                        }
                    }
                    return null;
                }
                catch (System.Exception)
                {
                    //ignore
                    return null;
                }
                
            }
        }

        //加载园区select列表
        protected SelectList PopulateGardenDropDownList(object selectedGarden = null)
        {
            var gardensQuery = _gardenManager.GetSelectListQuery();
            return new SelectList(gardensQuery, "Id", "Name", selectedGarden);
        }

        //加载模块Select列表
        protected SelectList PopulateModuleDropDownList(object selectedModule = null)
        {
            var modulesQuery = _moduleManager.GetSelectListQuery();
            return new SelectList(modulesQuery, "Id", "Name", selectedModule);
        }

        //加载子系统Select列表
        protected SelectList PopulateSubSystemDropDownList(object selectedSubSystem = null)
        {
            var systemQuery = _subSystemManager.GetSelectListQuery();
            return new SelectList(systemQuery, "Id", "Name", selectedSubSystem);
        }

        //加载服务分类Select列表
        protected SelectList PopulateServiceCategoryDropDownList(object selectedCategry = null)
        {
            var categoryQuery = _serviceCategoryManager.GetSelectListQuery();
            return new SelectList(categoryQuery, "Id", "Name", selectedCategry);
        }
    }
}
