using System.Linq;
using System.Threading.Tasks;
using DorllyService.Common;
using DorllyService.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DorllyServiceManager.Areas.Admin.Components
{
    public abstract class BaseViewComponent:ViewComponent
    {
        private IUserManager _userManager;
        public BaseViewComponent(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public abstract Task<IViewComponentResult> InvokeAsync();

        public async Task<Account> GetAccountAsync()
        {
            //从Session中获取用户对象
            if (HttpContext.Session.TryGetValue(AdminAuthorizeAttribute.AdminAuthenticationScheme, out byte[] accountData))
                return ByteConvertHelper.Bytes2Object<Account>(accountData);
            //Session过期 通过Cookies中的信息 重新获取用户对象 并存储于Session中
            var adminCookie = await HttpContext.AuthenticateAsync(AdminAuthorizeAttribute.AdminAuthenticationScheme);
            var principal = adminCookie.Principal;
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
    }
}
