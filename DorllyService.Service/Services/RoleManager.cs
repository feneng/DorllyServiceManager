using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DorllyService.Domain;
using DorllyService.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DorllyService.Service
{
    public class RoleManager:Repository<Role>,IRoleManager
    {
        private readonly ILogger<RoleManager> _logger;
        private readonly DorllyServiceManagerContext _context;
        public RoleManager(DorllyServiceManagerContext context,
            ILogger<RoleManager> logger
            ):base(context)
        {
            _logger = logger;
            _context = context;
        }

        public IQueryable<Role> GetIndexQuery()
        {
             return _context.Role.Include(r => r.RolePermissions).Include(r => r.UserRoles).AsNoTracking();
        }

        public override async Task<Role> LoadEntityAsNoTrackingAsync(Expression<Func<Role, bool>> predicate)
        {
            return await _context.Role
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                .Include(r => r.UserRoles)
                    .ThenInclude(ur => ur.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(predicate);
        }
    }
}
