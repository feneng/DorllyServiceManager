using System;
namespace DorllyServiceManager.Areas.Admin.ViewModels
{
    public class PermissionRoleData
    {
        public int PermissionId { get; set; }
        public int RoleId { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }           
        public bool Status { get; set; }
        public bool Assigned { get; set; }
    }
}
