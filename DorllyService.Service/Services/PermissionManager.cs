using System;
using DorllyService.Domain;
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


    }
}
