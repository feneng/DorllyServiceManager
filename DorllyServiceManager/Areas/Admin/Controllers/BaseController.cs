using System.Linq;
using DorllyService.Common;
using DorllyService.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DorllyServiceManager.Areas.Admin.Controllers
{

    public class BaseController : Controller
    {
        private readonly IUserManager _userManage;
        public BaseController(IUserManager userManage)
        {
            _userManage = userManage;
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
                            var salt = _userManage.GetUserSalt(account);
                            password = DesEncrypt.Decrypt(password, salt);
                            var user = _userManage.Login(account, password);
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
    }
}
