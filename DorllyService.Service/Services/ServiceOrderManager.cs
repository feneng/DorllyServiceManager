using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DorllyService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DorllyService.Service
{
    public class ServiceOrderManager : Repository<Order>, IServiceOrderManager
    {

        private readonly ILogger<ServiceOrderManager> _logger;
        private readonly DorllyServiceManagerContext _context;
        public ServiceOrderManager(DorllyServiceManagerContext context,
            ILogger<ServiceOrderManager> logger
            ) : base(context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// 加载自己定义排序分页实体列表
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="asc">asc/desc</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页的条数</param>      
        /// <returns></returns>
        public override IEnumerable<Order> LoadEntityEnumerable<S>(Expression<Func<Order, bool>> predicate,
            Expression<Func<Order, S>> orderby, string asc, int pageIndex, int pageSize) where S : struct
        {
            pageIndex--;
            return asc.Equals(nameof(asc)) ?
                _context.Set<Order>().Where(predicate).OrderBy(orderby).Skip(pageIndex * pageSize).Take(pageSize)
                .Include(o=>o.BelongGarden).AsNoTracking()
                : _context.Set<Order>().Where(predicate).OrderByDescending(orderby).Skip(pageSize * pageIndex).Take(pageSize)
                .Include(o => o.BelongGarden).AsNoTracking();
        }
        public override IEnumerable<Order> LoadEntityEnumerable(Expression<Func<Order, bool>> predicate, Expression<Func<Order, string>> orderby, string asc, int pageIndex, int pageSize)
        {
            pageIndex--;
            return asc.Equals(nameof(asc)) ?
                _context.Set<Order>().Where(predicate).OrderBy(orderby).Skip(pageIndex * pageSize).Take(pageSize).Include(o => o.BelongGarden).AsNoTracking()
                : _context.Set<Order>().Where(predicate).OrderByDescending(orderby).Skip(pageSize * pageIndex).Take(pageSize).Include(o => o.BelongGarden).AsNoTracking();
        }
    }
}
