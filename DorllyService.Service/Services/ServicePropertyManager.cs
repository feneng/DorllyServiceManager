using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DorllyService.Domain;
using DorllyService.IService;
using Microsoft.EntityFrameworkCore;
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

        public override async Task<ServiceProperty> LoadEntityAsync(Expression<Func<ServiceProperty, bool>> predicate) {
           return  await _context.Set<ServiceProperty>().Include(sp=>sp.Categories).Where(predicate).SingleOrDefaultAsync();
        }
    }
}
