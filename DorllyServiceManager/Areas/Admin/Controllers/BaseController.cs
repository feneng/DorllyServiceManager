using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DorllyService.Common;
using DorllyService.Domain;
using DorllyService.IService;
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
        private readonly IServicePropertyManager _servicePropertyManager;

        public BaseController(IUserManager userManager,
            IGardenManager gardenManager=null,
            IModuleManager moduleManager=null,
            ISubSystemManager subSystemManager=null,
            IServiceCategoryManager serviceCategoryManager=null,
            IServicePropertyManager servicePropertyManager = null
            )
        {
            _userManager = userManager;
            _gardenManager = gardenManager;
            _moduleManager = moduleManager;
            _subSystemManager = subSystemManager;
            _serviceCategoryManager = serviceCategoryManager;
            _servicePropertyManager = servicePropertyManager;
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

        //加载模块Select列表 TODO:这里实际上是树形列表，需要改进显示样式
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

        //加载服务分类Select列表  TODO:这里实际上是树形列表，需要改进显示样式
        protected SelectList PopulateServiceCategoriesDropDownList(object selectedCategory = null)
        {
            var categoryQuery = _serviceCategoryManager.GetSelectListQuery();
            return new SelectList(categoryQuery, "Id", "Name", selectedCategory);
        }

        /// <summary>
        /// 为 Select2 提供数据
        /// </summary>
        /// <param name="selectedCategories"></param>
        /// <returns></returns>
        protected async Task<IActionResult> GetServiceCategoriesForSelect2(int? propertyId = null)
        {
            var list = _serviceCategoryManager.GetSelectList<dynamic>(sc => new { id = sc.Id, text = sc.Name },
                sc => sc.Status);
            if (!propertyId.HasValue)
            {
                return Json(new { data = list});
            }
            var property =await _servicePropertyManager.FindEntityAsync(propertyId.Value);
            var selectedCategories = property.Categories.Select(c => c.CategoryId);
            
            return Json(new { data = list, tags = selectedCategories });
        }

        /// <summary>
        /// 加载时 Select2 数据
        /// 只取中类
        /// </summary>
        /// <returns></returns>
        protected IQueryable<ServiceCategory> GetServiceCategoriesForSelect2(IQueryable<ServiceCategory> querySource)
        {
            var list = _serviceCategoryManager.GetSelectList(sc =>sc,
                sc => sc.Status&&sc.Parent.Status && sc.Level == 2, querySource);
            return list;
        }
    }
}
