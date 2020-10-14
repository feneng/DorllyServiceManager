using System;
using System.Linq;
using DorllyService.Domain;
using DorllyService.IService;
using Microsoft.Extensions.Logging;

namespace DorllyService.Service
{
    public class SubSystemManager : Repository<SubSystem>, ISubSystemManager
    {
        private readonly DorllyServiceManagerContext _context;
        private readonly ILogger<SubSystemManager> _logger;
        public SubSystemManager(DorllyServiceManagerContext context,
            ILogger<SubSystemManager> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public IQueryable<SubSystem> GetIndexQuery()
        {
            return _context.SubSystem;
        }

        public IQueryable<SubSystem> GetSelectListQuery()
        {
            var query = from g in _context.SubSystem
                        orderby g.Id
                        where g.State == 1
                        select g;
            return query;
        }
    }
}
