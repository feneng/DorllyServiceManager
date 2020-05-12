using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DorllyService.Domain
{
    public class Permission
    {
        public int Id { get; set; }
        [StringLength(24)]
        [Display(Name="编码")]
        public string Code { get; set; }
        [StringLength(64)]
        [Display(Name = "权限名")]
        public string Name { get; set; }
        [Display(Name = "权限类型")]
        public byte Type { get; set; }
        [StringLength(128)]
        [Display(Name = "权限路径")]
        public string Path { get; set; }
        [StringLength(512)]
        [Display(Name = "权限描述")]
        public string Description { get; set; }
        [Display(Name = "排序")]
        public int Order { get; set; }
        [StringLength(64)]
        [Display(Name = "图标")]
        public string Icon { get; set; }
        [Display(Name = "所属模块")]
        public int BelongModuleId { get; set; }
        public Module BelongModule { get; set; }
        [Display(Name = "状态")]
        public bool Status { get; set; }
        [Display(Name = "拥有此权限的角色")]
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
