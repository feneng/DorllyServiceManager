using System;
namespace DorllyServiceManager.Areas.Admin.ComponentModels
{
    public class BreadcrumbsData
    {
        public BreadcrumbsData()
        {
        }

        public string Icon { get; set; }
        public string FirstCrumbs { get; set; }
        public string SecondCrumbs { get; set; }
    }
}
