using System.Threading.Tasks;
using DorllyService.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using DorllyService.Domain;

namespace DorllyServiceManager.Areas.Admin.Components
{
    public class MenuViewComponent : BaseViewComponent
    {
        public MenuViewComponent(IUserManager userManager) : base(userManager) { }

        public override async Task<IViewComponentResult> InvokeAsync()
        {
            var account = await GetAccountAsync();
            if (account == null)
            {
                return View();
            }

            var uri = $"/{RouteData.Values["controller"]}/{RouteData.Values["action"]}";
            PopulateMenuData(account.Modules, uri);
            return View("Default", account.Modules.OrderBy(m=>m.Order));
        }

        private void PopulateMenuData(IEnumerable<Module> modules, string uri)
        {
            var isSiblingCheck = false;
            foreach (var item in modules)
            {
                if (!isSiblingCheck && IsMenuCheck(item, uri))
                {
                    item.IsCheck = true;
                    isSiblingCheck = true;
                    if (item.Children.Any())
                        PopulateMenuData(item.Children, uri);
                }
            }
        }

        private bool IsMenuCheck(Module module, string uri)
        {
            if (!string.IsNullOrEmpty(module.Path) && module.Path.Contains(uri))
                return true;

            if (module.Children.Any())
            {
                foreach (var m in module.Children)
                {
                    var isCheck = IsMenuCheck(m, uri);
                    if (isCheck)
                        return true;
                    continue;
                }
            }
            return false;
        }
    }
}
