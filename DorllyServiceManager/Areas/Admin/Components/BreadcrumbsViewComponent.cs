using System.Threading.Tasks;
using DorllyServiceManager.Areas.Admin.ComponentModels;
using Microsoft.AspNetCore.Mvc;

namespace DorllyServiceManager.Areas.Admin.Components
{
    public class BreadcrumbsViewComponent: ViewComponent
    {

        public BreadcrumbsViewComponent()
        {
        }

        public IViewComponentResult Invoke()
        {
            return View("Default",
                 new BreadcrumbsData
                 {
                     FirstCrumbs = RouteData.Values["controller"].ToString(),
                     SecondCrumbs = RouteData.Values["action"].ToString()
                 });
        }
    }
}

