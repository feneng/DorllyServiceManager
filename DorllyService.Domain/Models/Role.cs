using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DorllyService.Domain
{
    public class Role
    {
        public int Id { get; set; }
        [StringLength(24)]
        [Display(Name ="编码")]
        [Required(ErrorMessage ="角色编码不能为空")]
        public string Code { get; set; }
        [StringLength(64)]
        [Display(Name = "角色名")]
        public string Name { get; set; }
        [StringLength(512)]
        [Display(Name = "角色描述")]
        public string Description { get; set; }
        [Display(Name = "状态")]
        public bool Status { get; set; }

        [Display(Name = "拥有此角色的用户")]
        public virtual ICollection<UserRole> UserRoles { get; set; }

        [Display(Name = "此角色拥有的权限")]
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
