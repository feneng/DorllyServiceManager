using System;
using System.ComponentModel.DataAnnotations;
using DorllyService.Domain;

namespace DorllyServiceManager.Areas.Admin.ViewModels
{
    public class RolePermissionData
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        [Display(Name = "权限名")]
        public string PermissionName { get; set; }
        [Display(Name = "编码")]
        public string PermissionCode { get; set; }
        public int PermissionOrder { get; set; }
        public string PermissionIcon { get; set; }
        [Display(Name = "状态")]
        public bool Status { get; set; }
        public bool Assigned { get; set; }
    }
}
