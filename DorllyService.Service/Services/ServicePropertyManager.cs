using System;
using System.Linq;
using DorllyService.Domain;
using Microsoft.Extensions.Logging;

namespace DorllyService.Service
{
    public class ServicePropertyManager : Repository<ServiceProperty>, IServicePropertyManager
    {
        private readonly ILogger<ServicePropertyManager> _logger;
        private readonly DorllyServiceManagerContext _context;
        public ServicePropertyManager(DorllyServiceManagerContext context,
            ILogger<ServicePropertyManager> logger
            ) : base(context)
        {
            _logger = logger;
            _context = context;
        }

        public IQueryable<ServiceProperty> GetIndexQuery()
        {
            return _context.ServiceProperty;
        }
    }
}
