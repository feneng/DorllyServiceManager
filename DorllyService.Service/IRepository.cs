﻿using Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace DorllyService.Service
{
    public interface IRepository<T> where T : class, new()
    {
        #region 数据对象操作
        DbContext Context { get; }
        //DbConfig Config { get; }

        DbSet<T> dbSet { get; }
        //DbContextTransaction Transaction { get; set; }
        bool Committed { get; set; }

        void Commit();
        void Rollback();
        #endregion

        #region 单模型操作
        T Get(Expression<Func<T, bool>> predicate);
        bool Save(T entity);
        bool Update(T entity);
        bool SaveOrUpdate(T entity, bool isEdit);
        int Delete(Expression<Func<T, bool>> predicate = null);
        int DeleteBySql(string sql, params DbParameter[] para);
        bool IsExist(Expression<Func<T, bool>> predicate);
        bool IsExist(string sql, params DbParameter[] para);
        #endregion

        #region 多模型操作
        /// <summary>
        /// 增加多模型数据，指定独立模型集合
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        int SaveList<T1>(List<T1> t) where T1 : class;
        /// <summary>
        /// 增加多模型数据，与当前模型一致
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int SaveList(List<T> t);
        /// <summary>
        /// 更新多模型，指定独立模型集合
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        int UpdateList<T1>(List<T1> t) where T1 : class;
        /// <summary>
        /// 更新多模型，与当前模型一致
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int UpdateList(List<T> t);
        /// <summary>
        /// 批量删除数据，当前模型
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int DeleteList(List<T> t);
        /// <summary>
        /// 批量删除数据，独立模型
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        int DeleteList<T1>(List<T1> t) where T1 : class;
        #endregion

        #region 存储过程操作
        /// <summary>
        /// 执行增删改存储过程
        /// </summary>
        /// <param name="procname"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        object ExecuteProc(string procname, params DbParameter[] parameter);
        /// <summary>
        /// 执行查询的存储过程
        /// </summary>
        /// <param name="procname"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        object ExecuteQueryProc(string procname, params DbParameter[] parameter);
        #endregion

        #region 查询多条数据
        /// <summary>
        /// 获取集合 IQueryable
        /// </summary>
        /// <returns></returns>
        IQueryable<T> LoadAll(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 获取集合 IList
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<T> LoadListAll(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 获取DbQuery的列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        DbSet<T> LoadQueryAll(Expression<Func<T, bool>> predicate);
        /// <summary>
        ///  获取IEnumerable列表
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        IEnumerable<T> LoadEnumerableAll(string sql, params DbParameter[] para);
        /// <summary>
        /// 获取数据动态集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        System.Collections.IEnumerable LoadEnumerable(string sql, params DbParameter[] para);
        List<T> SelectBySql(string sql, params DbParameter[] para);
        List<T1> SelectBySql<T1>(string sql, params DbParameter[] para);
        /// <summary>
        /// 可指定返回结果、排序、查询条件的通用查询方法，返回实体对象
        /// </summary>
        /// <typeparam name="TEntity">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <typeparam name="TResult">数据结果，一般为object</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>实体集合</returns>
        List<TResult> QueryEntity<TEntity, TOrderBy, TResult>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TOrderBy>> orderby, Expression<Func<TEntity, TResult>> selector, bool IsAsc)
             where TEntity : class
             where TResult : class;
        /// <summary>
        /// 可指定返回结果、排序、查询条件的通用查询方法，返回Object对象
        /// </summary>
        /// <typeparam name="TEntity">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>自定义实体集合</returns>
        List<object> QueryObject<TEntity, TOrderBy>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TOrderBy>> orderby, Func<IQueryable<TEntity>, List<object>> selector, bool IsAsc)
            where TEntity : class;
        /// <summary>
        /// 可指定返回结果、排序、查询条件的通用查询方法，返回动态类对象
        /// </summary>
        /// <typeparam name="TEntity">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>动态类对象</returns>
        dynamic QueryDynamic<TEntity, TOrderBy>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TOrderBy>> orderby, Func<IQueryable<TEntity>, List<object>> selector, bool IsAsc)
             where TEntity : class;
        #endregion

        #region 分页查询
        /// <summary>
        /// 通过SQL分页
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        IList<T1> PageByListSql<T1>(string sql, IList<DbParameter> parameters, PageCollection page);
        IList<T> PageByListSql(string sql, IList<DbParameter> parameters, PageCollection page);
        /// <summary>
        /// 通用EF分页，默认显示20条记录
        /// </summary>
        /// <typeparam name="TEntity">实体模型</typeparam>
        /// <typeparam name="TOrderBy">排序类型</typeparam>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">显示条数</param>
        /// <param name="where">过滤条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">结果集合</param>
        /// <param name="isAsc">排序方向true正序 false倒序</param>
        /// <returns>自定义实体集合</returns>
        PageInfo<object> Query<TEntity, TOrderBy>
            (int index, int pageSize,
            Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TOrderBy>> orderby,
            Func<IQueryable<TEntity>, List<object>> selector,
            bool IsAsc)
            where TEntity : class;
        /// <summary>
        /// 对IQueryable对象进行分页逻辑处理，过滤、查询项、排序对IQueryable操作
        /// </summary>
        /// <param name="t">Iqueryable</param>
        /// <param name="index">当前页</param>
        /// <param name="PageSize">每页显示多少条</param>
        /// <returns>当前IQueryable to List的对象</returns>
        Common.PageInfo<T> Query(IQueryable<T> query, int index, int PageSize);
        /// <summary>
        /// 普通SQL查询分页方法
        /// </summary>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">显示行数</param>
        /// <param name="tableName">表名/视图</param>
        /// <param name="field">获取项</param>
        /// <param name="filter">过滤条件</param>
        /// <param name="orderby">排序字段+排序方向</param>
        /// <param name="group">分组字段</param>
        /// <returns>结果集</returns>
        Common.PageInfo Query(int index, int pageSize, string tableName, string field, string filter, string orderby, string group, params DbParameter[] para);
        /// <summary>
        /// 简单的Sql查询分页
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        Common.PageInfo Query(int index, int pageSize, string sql, string orderby, params DbParameter[] para);
        /// <summary>
        /// 多表联合分页算法
        /// </summary>
        PageInfo Query(IQueryable query, int index, int pagesize);
        #endregion

        #region ADO.NET增删改查方法
        /// <summary>
        /// 执行增删改方法,含事务处理
        /// </summary>
        object ExecuteSqlCommand(string sql, params DbParameter[] para);
        /// <summary>
        /// 执行多条SQL，增删改方法,含事务处理
        /// </summary>
        object ExecuteSqlCommand(Dictionary<string, object> sqllist);
        /// <summary>
        /// 执行查询方法,返回动态类，接收使用var，遍历时使用dynamic类型
        /// </summary>
        object ExecuteSqlQuery(string sql, params DbParameter[] para);
        #endregion
    }
}
