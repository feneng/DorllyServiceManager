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
    public class PropertyValueManager : Repository<PropertyValue>, IPropertyValueManager
    {
        private readonly ILogger<PropertyValueManager> _logger;
        private readonly DorllyServiceManagerContext _context;
        public PropertyValueManager(DorllyServiceManagerContext context,
            ILogger<PropertyValueManager> logger
            ) : base(context)
        {
            _logger = logger;
            _context = context;
        }

        public override async Task<List<PropertyValue>> LoadEntityListAsNoTrackingAsync(Expression<Func<PropertyValue, bool>> predicate)
        {
            return await _context.Set<PropertyValue>()
                .Include(pv=>pv.Property)
                .AsNoTracking().Where(predicate).ToListAsync();
        }
    }
}
