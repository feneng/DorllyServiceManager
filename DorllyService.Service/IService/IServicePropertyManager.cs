using System;
using System.Linq;
using DorllyService.Domain;

namespace DorllyService.Service
{
    public interface IServicePropertyManager : IRepository<ServiceProperty>
    {
        IQueryable<ServiceProperty> GetIndexQuery();
    }
}
