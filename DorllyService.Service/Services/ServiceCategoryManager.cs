using System;
using System.Collections.Generic;
using System.Linq;
using DorllyService.Common.Extensions;
using DorllyService.Domain;
using Microsoft.Extensions.Logging;

namespace DorllyService.Service
{
    public class ServiceCategoryManager:Repository<ServiceCategory>,IServiceCategoryManager
    {
        private DorllyServiceManagerContext _context;
        private ILogger<ServiceCategoryManager> _logger;
        public ServiceCategoryManager(DorllyServiceManagerContext context,ILogger<ServiceCategoryManager> logger):base(context)
        {
            _logger = logger;
            _context = context;
        }

        public IEnumerable<ServiceCategory> GetServiceCategoryTree()
        {
            var enumer = from s in _context.ServiceCategory orderby s.Id ascending select s;
            return enumer.AsEnumerable().ToTreeList(null);//.ToTreeStruct(null);
        }

        public IQueryable<ServiceCategory> GetSelectListQuery()
        {
            var query = from g in _context.ServiceCategory
                        orderby g.Id
                        where g.Status
                        select g;
            return query;
        }
    }
}
