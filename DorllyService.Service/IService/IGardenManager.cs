using System;
using System.Linq;
using DorllyService.Domain;

namespace DorllyService.Service
{
    public interface IGardenManager : IRepository<Garden>
    {
        IQueryable<Garden> GetIndexQuery();
        IQueryable<Garden> GetSelectListQuery();
    }
}
