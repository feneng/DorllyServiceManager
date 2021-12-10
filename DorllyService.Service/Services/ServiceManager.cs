using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DorllyService.Domain;
using DorllyService.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DorllyService.Service
{
    public class ServiceManager : Repository<Domain.Service>, IServiceManager
    {
        private readonly ILogger<ServiceManager> _logger;
        private readonly DorllyServiceManagerContext _context;
        public ServiceManager(DorllyServiceManagerContext context,
            ILogger<ServiceManager> logger
            ) : base(context)
        {
            _logger = logger;
            _context = context;
        }

        public IQueryable<Domain.Service> GetIndexQuery()
        {
            return _context.Service
                .Include(s => s.Category)
                    .ThenInclude(s=>s.Parent)
                .AsNoTracking();
        }
    }
}
