using System;
using System.Collections.Generic;
using System.Linq;
using DorllyService.Domain;

namespace DorllyService.Service
{
    public interface IServiceCategoryManager:IRepository<ServiceCategory>
    {
        IEnumerable<ServiceCategory> GetServiceCategoryTree();
        IQueryable<ServiceCategory> GetSelectListQuery();
    }
}
