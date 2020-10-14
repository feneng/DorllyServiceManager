using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DorllyService.Domain;
using DorllyService.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DorllyService.Service
{
    public class ServiceProviderManager: Repository<ServiceSupplier>, IServiceProviderManager
    {
        private readonly ILogger<ServiceProviderManager> _logger;
        private readonly DorllyServiceManagerContext _context;
        public ServiceProviderManager(DorllyServiceManagerContext context,
            ILogger<ServiceProviderManager> logger
            ) : base(context)
        {
            _logger = logger;
            _context = context;
        }

        public override async Task<List<ServiceSupplier>> LoadEntityAllAsync()
        {
            return await _context.Set<ServiceSupplier>()
                .Include(ss => ss.BelongGarden).AsTracking().ToListAsync();
        }
    }
}
