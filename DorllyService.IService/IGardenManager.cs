using System;
using System.Linq;
using DorllyService.Domain;

namespace DorllyService.IService
{
    public interface IGardenManager : IRepository<Garden>
    {
        IQueryable<Garden> GetIndexQuery();
        IQueryable<Garden> GetSelectListQuery();
    }
}
