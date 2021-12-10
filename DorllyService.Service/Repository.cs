using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DorllyService.Common;
using DorllyService.Domain;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace DorllyService.Service
{
    /// <summary>
    /// 数据操作基类，公共方法实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : BaseRepository, IRepository<T> where T : class, new()
    {
        private readonly DorllyServiceManagerContext _context;
        public Repository(DorllyServiceManagerContext context)
            : base(context)
        {
            _context = context;
        }

        #region 新增

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual void AddEntity(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        /// <summary>
        /// 异步新增实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task AddEntityAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        /// <summary>
        /// 新增实体列表
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        /// <summary>
        /// 异步新增实体列表
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual int DelEntity(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).Delete();
        }

        /// <summary>
        /// 异步删除实体
        /// </summary>
        /// <param name="predicate">实体</param>
        /// <returns></returns>
        public virtual async Task<int> DelEntityAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).DeleteAsync();
        }

        #endregion

        #region 修改

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual void UpdateEntity(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual int UpdateEntityRange(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> entity)
        {
            return _context.Set<T>().Where(predicate).Update(entity);
        }

        public virtual async Task<int> UpdateEntityRangeAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> entity)
        {
            return await _context.Set<T>().Where(predicate).UpdateAsync(entity);
        }

        #endregion

        #region 查询

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public virtual List<T> LoadEntityAll()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }

        /// <summary>
        /// 异步查询所有数据
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<T>> LoadEntityAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="predicate">lamda表达式</param>
        /// <returns></returns>
        public virtual IEnumerable<T> LoadEntityEnumerable(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        /// <summary>
        /// 根据条件查询实体列表不跟踪
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual List<T> LoadEntityListAsNoTracking(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).AsNoTracking().ToList();
        }

        /// <summary>
        /// 异步根据条件查询实体列表不跟踪
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> LoadEntityListAsNoTrackingAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// 根据主键查询实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public virtual T FindEntity(int id)
        {
            return _context.Set<T>().Find(id);
        }

        /// <summary>
        /// 根据主键异步查询实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public virtual async Task<T> FindEntityAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// 根据加载实体
        /// </summary>
        /// <param name="predicate">lamda表达式</param>
        /// <returns></returns>
        public virtual T LoadEntity(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).SingleOrDefault();
        }

        /// <summary>
        /// 根据条件异步加载实体
        /// </summary>
        /// <param name="predicate">lamda表达式</param>
        /// <returns></returns>
        public virtual async Task<T> LoadEntityAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 根据条件查询实体不跟踪
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual T LoadEntityAsNoTracking(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).AsNoTracking().SingleOrDefault();
        }

        /// <summary>
        /// 异步根据条件异步查询实体不跟踪
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<T> LoadEntityAsNoTrackingAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).AsNoTracking().SingleOrDefaultAsync();
        }
        /// <summary>
        /// 获取最大的一条数据
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public virtual T LoadEntityMaxSort(Expression<Func<T, int>> order)
        {
            return _context.Set<T>().OrderByDescending(order).FirstOrDefault();
        }
        /// <summary>
        /// 异步获取最大的一条数据
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public virtual async Task<T> LoadEntityMaxSortAsync(Expression<Func<T, int>> order)
        {
            return await _context.Set<T>().OrderByDescending(order).FirstOrDefaultAsync();
        }

        /// <summary>
        ///  Any 查找数据判断数据是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual bool AnyEntity(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Any(predicate);
        }

        /// <summary>
        ///  异步Any 查找数据判断数据是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyEntityAsync(Expression<Func<T, bool>> predicate)
        { 
            return await _context.Set<T>().AnyAsync(predicate);
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
        public virtual IEnumerable<T> LoadEntityEnumerable<S>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, S>> orderby, string asc, int pageIndex, int pageSize) where S : struct
        {
            pageIndex--;
            return asc.Equals(nameof(asc)) ?
                _context.Set<T>().Where(predicate).OrderBy(orderby).Skip(pageIndex * pageSize).Take(pageSize).AsNoTracking()
                : _context.Set<T>().Where(predicate).OrderByDescending(orderby).Skip(pageSize * pageIndex).Take(pageSize).AsNoTracking();
        }
        public virtual IEnumerable<T> LoadEntityEnumerable(Expression<Func<T, bool>> predicate, Expression<Func<T, string>> orderby, string asc, int pageIndex, int pageSize)
        {
            pageIndex--;
            return asc.Equals(nameof(asc)) ?
                _context.Set<T>().Where(predicate).OrderBy(orderby).Skip(pageIndex * pageSize).Take(pageSize).AsNoTracking()
                : _context.Set<T>().Where(predicate).OrderByDescending(orderby).Skip(pageSize * pageIndex).Take(pageSize).AsNoTracking();
        }

        /// <summary>
        /// get Jquery.DataTables paging data
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="select"></param>
        /// <param name="predicate"></param>
        /// <param name="pageInfo"></param>
        /// <param name="querySource"></param>
        /// <returns></returns>
        public async Task<List<TResult>> GetPageListAsync<TResult>(
            Expression<Func<T, TResult>> select, Expression<Func<T, bool>> predicate,
            PageInfo<T> pageInfo,IQueryable<T> querySource=null)
            where TResult: class,new()
        {
            var query =querySource is null? _context.Set<T>().Where(predicate): querySource.Where(predicate);
            pageInfo.recordsTotal = query.Count();
            var parameter = Expression.Parameter(typeof(T), "o");
            if (pageInfo.order != null && pageInfo.order.Count() > 0)
            {
                var prefix = "Order";
                for (int i = 0; i < pageInfo.order.Count(); i++)
                {
                    var property = typeof(T).GetProperty(pageInfo.columns[pageInfo.order[i].column].data);
                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var orderByExp = Expression.Lambda(propertyAccess, parameter);
                    var orderName = pageInfo.order[i].dir.Equals("desc") ? $"{prefix}ByDescending" :$"{prefix}By";
                    var resultExp = Expression.Call(typeof(Queryable), orderName, new Type[] { typeof(T), property.PropertyType }, query.Expression, Expression.Quote(orderByExp));
                    query = query.Provider.CreateQuery<T>(resultExp);
                    prefix = "Then";
                }
            }

            pageInfo.recordsFiltered = query.Count();
            return await query.Skip((pageInfo.pageIndex - 1) * pageInfo.length).Take(pageInfo.length).Select(select).ToListAsync();
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
        public virtual async Task<List<T>> LoadEntityListAsync(Expression<Func<T, bool>> predicate,
            Expression<Func<T, string>> orderby, string asc, int pageIndex, int pageSize)
        {
            pageIndex--;
            return asc.Equals(nameof(asc)) ?
                 await _context.Set<T>().Where(predicate).OrderBy(orderby)
                 .Skip(pageIndex * pageSize).Take(pageSize).AsNoTracking().ToListAsync()
                 : await _context.Set<T>().Where(predicate).OrderByDescending(orderby)
                 .Skip(pageIndex * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
        }
        public virtual async Task<List<T>> LoadEntityListAsync<S>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, S>> orderby, string asc, int pageIndex, int pageSize) where S : struct
        {
            pageIndex--;
            return asc.Equals(nameof(asc)) ?
                 await _context.Set<T>().Where(predicate).OrderBy(orderby)
                 .Skip(pageIndex * pageSize).Take(pageSize).AsNoTracking().ToListAsync()
                : await _context.Set<T>().Where(predicate).OrderByDescending(orderby)
                .Skip(pageIndex * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
        }
        #region 求平均，求总计
        public virtual int? GetSum(Expression<Func<T, bool>> predicate, Expression<Func<T, int?>> selector)
        {
            return _context.Set<T>().Where(predicate).Sum(selector);
        }
        public virtual double? GetSum(Expression<Func<T, bool>> predicate, Expression<Func<T, double?>> selector)
        {
            return _context.Set<T>().Where(predicate).Sum(selector);
        }
        public virtual float? GetSum(Expression<Func<T, bool>> predicate, Expression<Func<T, float?>> selector)
        {
            return _context.Set<T>().Where(predicate).Sum(selector);
        }
        public virtual decimal? GetSum(Expression<Func<T, bool>> predicate, Expression<Func<T, decimal?>> selector)
        {
            return _context.Set<T>().Where(predicate).Sum(selector);
        }
        //public virtual S GetSum<S>(Expression<Func<T, bool>> predicate, Expression<Func<T, S>> selector) where S : struct
        //{
        //    return _context.Set<T>().Where(predicate).Sum(selector);
        //}

        public virtual double? GetAvg(Expression<Func<T, bool>> predicate, Expression<Func<T, int?>> selector)
        {
            return _context.Set<T>().Where(predicate).Average(selector);
        }
        public virtual double? GetAvg(Expression<Func<T, bool>> predicate, Expression<Func<T, double?>> selector)
        {
            return _context.Set<T>().Where(predicate).Average(selector);
        }
        public virtual float? GetAvg(Expression<Func<T, bool>> predicate, Expression<Func<T, float?>> selector)
        {
            return _context.Set<T>().Where(predicate).Average(selector);
        }
        public virtual decimal? GetAvg(Expression<Func<T, bool>> predicate, Expression<Func<T, decimal?>> selector)
        {
            return _context.Set<T>().Where(predicate).Average(selector);
        }
        //public virtual S GetAvg<S>(Expression<Func<T, bool>> predicate, Expression<Func<T, S>> selector) where S : struct
        //{
        //    return _context.Set<T>().Where(predicate).Average(selector);
        //}
        #endregion

        #region 查最大
        //int? GetMax(Expression<Func<T, int?>> max);
        //double? GetMax(Expression<Func<T, double?>> max);
        //decimal? GetMax(Expression<Func<T, decimal?>> max);
        //DateTime? GetMax(Expression<Func<T, DateTime?>> max);
        public virtual S GetMax<S>(Expression<Func<T, S>> max) where S : struct
        {
            return _context.Set<T>().Max(max);
        }
        #endregion
        /// <summary>
        /// 查询实体数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual int GetEntitiesCount(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).Count();
        }
        /// <summary>
        /// 异步查询实体数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<int> GetEntitiesCountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).CountAsync();
        }
        /// <summary>
        /// 使用sql脚本查询实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual List<T> GetModeListlBySql(FormattableString sql)
        {
            return _context.Set<T>().FromSqlInterpolated(sql).ToList();
        }

        /// <summary>
        /// 使用sql脚本异步查询实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> GetModeListlBySqlAsync(FormattableString sql)
        {
            return await _context.Set<T>().FromSqlInterpolated(sql).ToListAsync();
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        public virtual void ExecFromSql(FormattableString sql)
        {
            _context.Set<T>().FromSqlInterpolated(sql);
        }

        #endregion
        /// <summary>
        /// 主从同步更换数据库连接
        /// </summary>
        public void SqlMasterSlaveConn()
        {
            var connection = JsonConfigurationHelper.GetAppSettings<ConnectionService>("appsettings.json", "ConnectionStrings");
            _context.Database.GetDbConnection().ConnectionString = connection.TheOtherSqlServiceConnection;
        }


        public IQueryable<TResult> GetSelectList<TResult>(Expression<Func<T, TResult>> select,
            Expression<Func<T, bool>> predicate, IQueryable<T> querySource = null) where TResult:class
        {
            var query = querySource is null ? _context.Set<T>().Where(predicate) : querySource.Where(predicate);
            return query.Select(select);
        }
    }
}
