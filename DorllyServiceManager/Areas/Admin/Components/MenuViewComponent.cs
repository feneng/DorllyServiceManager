using System.Threading.Tasks;
using DorllyService.Service;
using Microsoft.AspNetCore.Mvc;

namespace DorllyServiceManager.Areas.Admin.Components
{
    public class MenuViewComponent:BaseViewComponent
    {
        public MenuViewComponent(IUserManager userManager):base(userManager){}

        public override async Task<IViewComponentResult> InvokeAsync()
        {
            var account = await GetAccountAsync();
            if (account==null)
            {
                return View();
            }
            return View("Default",account.Modules);
        } 
    }
}
