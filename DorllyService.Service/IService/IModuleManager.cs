using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DorllyService.Domain;

namespace DorllyService.Service
{
    public interface IModuleManager:IRepository<Module>
    {
        IEnumerable<Module> GetModuleTree();
        IQueryable<Module> GetSelectListQuery();
    }
}
