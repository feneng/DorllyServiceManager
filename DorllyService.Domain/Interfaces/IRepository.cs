using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DorllyService.Common;
using DorllyService.Common.Interfaces;

namespace DorllyService.Domain
{
    public interface IRepository<T> : IBaseRepository where T : class, new()
    {
        #region 新增

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void AddEntity(T entity);

        /// <summary>
        /// 异步新增实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddEntityAsync(T entity);

        /// <summary>
        /// 新增实体列表
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        void AddRange(IEnumerable<T> entities);

        /// <summary>
        /// 异步新增实体列表
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task AddRangeAsync(IEnumerable<T> entities);

        #endregion

        #region 删除

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int DelEntity(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 异步删除实体
        /// </summary>
        /// <param name="predicate">实体</param>
        /// <returns></returns>
        Task<int> DelEntityAsync(Expression<Func<T, bool>> predicate);

        #endregion

        #region 修改

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void UpdateEntity(T entity);

        int UpdateEntityRange(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> entity);

        Task<int> UpdateEntityRangeAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> entity);

        #endregion

        #region 查询

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        List<T> LoadEntityAll();

        /// <summary>
        /// 异步查询所有数据
        /// </summary>
        /// <returns></returns>
        Task<List<T>> LoadEntityAllAsync();

        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="predicate">lamda表达式</param>
        /// <returns></returns>
        IEnumerable<T> LoadEntityEnumerable(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 根据条件查询实体列表不跟踪
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<T> LoadEntityListAsNoTracking(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 异步根据条件查询实体列表不跟踪
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<List<T>> LoadEntityListAsNoTrackingAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 根据加载实体
        /// </summary>
        /// <param name="predicate">lamda表达式</param>
        /// <returns></returns>
        T LoadEntity(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 根据主键查询实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        T FindEntity(int id);

        /// <summary>
        /// 根据主键异步查询实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<T> FindEntityAsync(int id);

        /// <summary>
        /// 根据条件异步加载实体
        /// </summary>
        /// <param name="predicate">lamda表达式</param>
        /// <returns></returns>
        Task<T> LoadEntityAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 根据条件查询实体不跟踪
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T LoadEntityAsNoTracking(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 异步根据条件异步查询实体不跟踪
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<T> LoadEntityAsNoTrackingAsync(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 获取最大的一条数据
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        T LoadEntityMaxSort(Expression<Func<T, int>> order);
        /// <summary>
        /// 异步获取最大的一条数据
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Task<T> LoadEntityMaxSortAsync(Expression<Func<T, int>> order);

        /// <summary>
        ///  Any 查找数据判断数据是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool AnyEntity(Expression<Func<T, bool>> predicate);

        /// <summary>
        ///  异步Any 查找数据判断数据是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<bool> AnyEntityAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 加载自己定义排序分页实体列表
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="asc">asc/desc</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页的条数</param>      
        /// <returns></returns>
        IEnumerable<T> LoadEntityEnumerable<S>(Expression<Func<T, bool>> predicate, Expression<Func<T, S>> orderby, string asc, int pageIndex, int pageSize) where S:struct;
        IEnumerable<T> LoadEntityEnumerable(Expression<Func<T, bool>> predicate, Expression<Func<T, string>> orderby, string asc, int pageIndex, int pageSize);
        


        /// <summary>
        /// 加载自己定义排序分页实体列表
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="asc">asc/desc</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页的条数</param>      
        /// <returns></returns>
        Task<List<T>> LoadEntityListAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, string>> orderby, string asc, int pageIndex, int pageSize);
        Task<List<T>> LoadEntityListAsync<S>(Expression<Func<T, bool>> predicate, Expression<Func<T, S>> orderby, string asc, int pageIndex, int pageSize) where S:struct;
        Task<List<TResult>> GetPageListAsync<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> predicate, PageInfo<T> pageInfo,IQueryable<T> querySource=null) where TResult :class,new();

        #region 求平均，求总计
        int? GetSum(Expression<Func<T, bool>> predicate, Expression<Func<T, int?>> sum);
        double? GetSum(Expression<Func<T, bool>> predicate, Expression<Func<T, double?>> sum);
        float? GetSum(Expression<Func<T, bool>> predicate, Expression<Func<T, float?>> sum);
        decimal? GetSum(Expression<Func<T, bool>> predicate, Expression<Func<T, decimal?>> sum);
        //S GetSum<S>(Expression<Func<T, bool>> predicate, Expression<Func<T, S>> sum) where S:struct;

        double? GetAvg(Expression<Func<T, bool>> predicate, Expression<Func<T, int?>> avg);
        double? GetAvg(Expression<Func<T, bool>> predicate, Expression<Func<T, double?>> avg);
        float? GetAvg(Expression<Func<T, bool>> predicate, Expression<Func<T, float?>> avg);
        decimal? GetAvg(Expression<Func<T, bool>> predicate, Expression<Func<T, decimal?>> avg);
        //S GetAvg<S>(Expression<Func<T, bool>> predicate, Expression<Func<T, S>> avg) where S : struct;
        #endregion

        #region 查最大
        //int? GetMax(Expression<Func<T, int?>> max);
        //double? GetMax(Expression<Func<T, double?>> max);
        //decimal? GetMax(Expression<Func<T, decimal?>> max);
        //DateTime? GetMax(Expression<Func<T, DateTime?>> max);
        S GetMax<S>(Expression<Func<T, S>> max) where S : struct;
        #endregion
        /// <summary>
        /// 查询实体数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int GetEntitiesCount(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 异步查询实体数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int> GetEntitiesCountAsync(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 使用sql脚本查询实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        List<T> GetModeListlBySql(FormattableString sql);

        /// <summary>
        /// 使用sql脚本异步查询实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<List<T>> GetModeListlBySqlAsync(FormattableString sql);

        
        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        void ExecFromSql(FormattableString sql);

        #endregion
        /// <summary>
        /// 主从同步更换数据库连接
        /// </summary>
        void SqlMasterSlaveConn();

        IQueryable<TResult> GetSelectList<TResult>(Expression<Func<T, TResult>> select,
            Expression<Func<T, bool>> predicate, IQueryable<T> querySource = null) where TResult : class;
    }
}
