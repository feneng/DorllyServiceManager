using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DorllyServiceManager.Models
{
    public class Permission
    {
        public int Id { get; set; }
        [StringLength(24)]
        public string Code { get; set; }
        [StringLength(64)]
        public string Name { get; set; }
        public byte Type { get; set; }
        [StringLength(128)]
        public string Path { get; set; }
        [StringLength(512)]
        public string Description { get; set; }
        public int Order { get; set; }
        [StringLength(64)]
        public string Icon { get; set; }
        public int BelongModuleId { get; set; }
        public Module BelongModule { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
