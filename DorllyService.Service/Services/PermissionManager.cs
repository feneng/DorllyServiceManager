using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DorllyService.Domain;
using DorllyService.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DorllyService.Service
{
    public class PermissionManager : Repository<Permission>, IPermissionManager
    {
        private readonly DorllyServiceManagerContext _context;
        private readonly ILogger<PermissionManager> _logger;
        public PermissionManager(
            DorllyServiceManagerContext context,
            ILogger<PermissionManager> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public override async Task<Permission> LoadEntityAsNoTrackingAsync(Expression<Func<Permission, bool>> predicate)
        {
            return await _context.Permission
                .Include(p=>p.RolePermissions)
                .ThenInclude(rp=>rp.Role)
                .Include(p=>p.BelongModule)
                .AsNoTracking()
                .SingleOrDefaultAsync(predicate);
        }
    }
}
