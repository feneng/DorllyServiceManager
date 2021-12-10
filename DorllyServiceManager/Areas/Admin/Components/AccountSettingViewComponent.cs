using System;
using System.Threading.Tasks;
using DorllyService.IService;
using DorllyServiceManager.Areas.Admin.ComponentModels;
using Microsoft.AspNetCore.Mvc;

namespace DorllyServiceManager.Areas.Admin.Components
{
    public class AccountSettingViewComponent : BaseViewComponent
    {
        public AccountSettingViewComponent(IUserManager userManager) : base(userManager) { }

        public override async Task<IViewComponentResult> InvokeAsync()
        {
            var account = await GetAccountAsync();
            if (account == null)
            {
                return View();
            }
            //TODO:获取用户日程表 Notification

            //TODO:获取用户消息列表 Messages

            //TODO:获取用户任务列表 Tasks

            return View("Default",
                new AccountData
                {
                    Avatar = account.Avatar,
                    UserEmail = account.Email,
                    UserName = account.Name
                });
        }
    }
}
