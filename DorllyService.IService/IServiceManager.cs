using System;
using System.Linq;
using DorllyService.Domain;

namespace DorllyService.IService
{
    public interface IServiceManager : IRepository<Domain.Service>
    {
        IQueryable<Domain.Service> GetIndexQuery();
    }
}

