using System;
using System.Linq;
using DorllyService.Domain;
using Microsoft.Extensions.Logging;

namespace DorllyService.Service
{
    public class GardenManager : Repository<Garden>, IGardenManager
    {
        private readonly DorllyServiceManagerContext _context;
        private readonly ILogger<GardenManager> _logger;
        public GardenManager(DorllyServiceManagerContext context,
            ILogger<GardenManager> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public IQueryable<Garden> GetIndexQuery()
        {
            return _context.Garden;
        }

        public IQueryable<Garden> GetSelectListQuery()
        {
            var query = from g in _context.Garden
                        orderby g.Id
                        where g.State == 1
                        select g;
            return query;
        }
    }
}
