﻿using System.Text.Json.Serialization;

namespace DorllyService.Domain
{
    public class RolePermission
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
