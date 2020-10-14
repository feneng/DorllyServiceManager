using System;
using System.Linq;
using DorllyService.Domain;

namespace DorllyService.IService
{
    public interface ISubSystemManager : IRepository<SubSystem>
    {
        IQueryable<SubSystem> GetIndexQuery();
        IQueryable<SubSystem> GetSelectListQuery();
    }
}
