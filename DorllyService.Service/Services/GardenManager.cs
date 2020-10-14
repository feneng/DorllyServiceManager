using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DorllyService.Domain;
using DorllyService.IService;
using Microsoft.Extensions.Logging;

namespace DorllyService.Service
{
    public class GardenManager : Repository<Garden>, IGardenManager
    {
        private readonly DorllyServiceManagerContext _context;
        private readonly ILogger<GardenManager> _logger;
        public GardenManager(DorllyServiceManagerContext context,
            ILogger<GardenManager> logger) : base(context)
        {
            _context = context;
            _logger = logger;
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
        public override IEnumerable<Garden> LoadEntityEnumerable<S>(Expression<Func<Garden, bool>> predicate,
            Expression<Func<Garden, S>> orderby, string asc, int pageIndex, int pageSize) where S : struct
        {
            pageIndex--;
            return asc.Equals(nameof(asc)) ?
                _context.Set<Garden>().Where(predicate).OrderBy(orderby).Skip(pageIndex * pageSize).Take(pageSize)
                : _context.Set<Garden>().Where(predicate).OrderByDescending(orderby).Skip(pageSize * pageIndex).Take(pageSize);
        }
        public override IEnumerable<Garden> LoadEntityEnumerable(Expression<Func<Garden, bool>> predicate, Expression<Func<Garden, string>> orderby, string asc, int pageIndex, int pageSize)
        {
            pageIndex--;
            return asc.Equals(nameof(asc)) ?
                _context.Set<Garden>().Where(predicate).OrderBy(orderby).Skip(pageIndex * pageSize).Take(pageSize)
                : _context.Set<Garden>().Where(predicate).OrderByDescending(orderby).Skip(pageSize * pageIndex).Take(pageSize);
        }

        public IQueryable<Garden> GetIndexQuery()
        {
            return _context.Garden;
        }

        public IQueryable<Garden> GetSelectListQuery()
        {
            var query = from g in _context.Garden
                        orderby g.Id
                        where g.State == 1
                        select g;
            return query;
        }
    }
}
