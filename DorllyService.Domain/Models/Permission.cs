using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using DorllyService.Common.Enums;

namespace DorllyService.Domain
{
    public class Permission
    {
        public int Id { get; set; }
        [StringLength(24)]
        [Display(Name="编码")]
        [Required(ErrorMessage ="权限编码不能为空")]
        public string Code { get; set; }
        [StringLength(64)]
        [Display(Name = "权限名")]
        public string Name { get; set; }
        [Display(Name = "权限类型")]
        public byte Type { get; set; }
        [StringLength(128)]
        [Display(Name = "权限路径")]
        public string Path { get; set; }
        [StringLength(32)]
        [Display(Name = "权限区域")]
        public string Area { get; set; }
        [StringLength(32)]
        [Display(Name = "权限控制器")]
        public string Controller { get; set; }
        [StringLength(32)]
        [Display(Name = "权限动作")]
        public string Action { get; set; }
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

        [NotMapped]
        [Display(Name = "状态")]
        public string StateName
        {
            get
            {
                return ((StateStatus)(Status?1:0)).ToString();
            }
        }

        [NotMapped]
        [Display(Name = "权限类型")]
        public string TypeName
        {
            get
            {
                return Type==1?"实际权限":"虚拟权限";
            }
        }
    }
}
