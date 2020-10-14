using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DorllyService.Domain;
using DorllyService.IService;
using Microsoft.Extensions.Logging;
using DorllyService.Common.Extensions;

namespace DorllyService.Service
{
    public class ModuleManager:Repository<Module>,IModuleManager
    {
        private readonly DorllyServiceManagerContext _context;
        private readonly ILogger<ModuleManager> _logger;
        public ModuleManager(DorllyServiceManagerContext context,
            ILogger<ModuleManager> logger):base(context)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Module> GetModuleTree()
        {
            var enumer = from s in _context.Module orderby s.Order ascending select s;
            return enumer.AsEnumerable().ToTreeList(null);//.ToTreeStruct(null);
        }

        public IQueryable<Module> GetSelectListQuery()
        {
            var query = from g in _context.Module
                        orderby g.Order
                        where g.Status
                        select g;
            return query;
        }
    }
}
