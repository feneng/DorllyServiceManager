using System;
using DorllyService.Domain;
using DorllyService.IService;
using Microsoft.Extensions.Logging;

namespace DorllyService.Service
{
    public class ContractManager:Repository<Contract>,IContractManager
    {
        private DorllyServiceManagerContext _context;
        private ILogger<ContractManager> _logger;
        public ContractManager(DorllyServiceManagerContext context, ILogger<ContractManager> logger) :base(context)
        {
            _context = context;
            _logger = logger;
        }
    }
}
