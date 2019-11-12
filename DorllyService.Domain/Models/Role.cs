using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DorllyService.Domain
{
    public class Role
    {
        public int Id { get; set; }
        [StringLength(24)]
        public string Code { get; set; }
        [StringLength(64)]
        public string Name { get; set; }
        [StringLength(512)]
        public string Description { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
