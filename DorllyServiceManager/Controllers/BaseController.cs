using DorllyService.Service;
using DorllyServiceManager.Extensions;
using DorllyServiceManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DorllyServiceManager.Controllers
{
    public class BaseController:Controller
    {
        #region 公用变量
        /// <summary>
        /// 查询关键词
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        /// 视图传递的分页页码
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 视图传递的分页条数
        /// </summary>
        public int PageSize { get; set; }
        #endregion

        #region 用户对象
        /// <summary>
        /// 获取当前用户对象
        /// </summary>
        public Account CurrentUser
        {
            get
            {
                var currentUser = HttpContext.Session.Get<Account>("CurrentUser");
                //从Session中获取用户对象
                if (currentUser != null)
                {
                    return currentUser;
                }
                //Session过期 通过Cookies中的信息 重新获取用户对象 并存储于Session中
                //var account = userManage.GetAccountByCookie();
                var account = new Account();
                HttpContext.Session.Set<Account>("CurrentUser",account);
                return account;
            }
        }

        //public IEnumerable<MenuViewModel> CurrentModules
        //{
        //    get
        //    {
        //        //从Session中获取模块对象列表
        //        if (SessionHelper.GetSession("CurrentModules") != null)
        //        {
        //            return SessionHelper.GetSession("CurrentModules") as IEnumerable<MenuViewModel>;
        //        }
        //        //Session过期 通过Cookies中的信息 重新获取模块对象列表 并存储于Session中
        //        string systems = "1";
        //        IQueryable<Domain.sys_module> query = ModuleManage.LoadAll(null);
        //        query = !string.IsNullOrEmpty(systems) ? query.Where(p => p.fk_belongsystem == systems) : query.Where(p => CurrentUser.SystemId.Any(e => e == p.fk_belongsystem));
        //        IEnumerable<MenuViewModel> currentModules = ModuleManage.RecursiveModule(query.ToList()).Select(p => new MenuViewModel
        //        {
        //            MenuId = p.id,
        //            MenuName = p.name,
        //            MenuPath = p.modulepath,
        //            MenuType = p.moduletype,
        //            Icon = p.icon,
        //            ParentId = p.parentid,
        //            Children = null
        //        });

        //        IEnumerable<MenuViewModel> modules = RecursiveModule(currentModules, 0);
        //        SessionHelper.SetSession("CurrentModules", modules);
        //        return modules;
        //    }
        //}

        //private IEnumerable<MenuViewModel> RecursiveModule(IEnumerable<MenuViewModel> list, int parentId)
        //{
        //    MenuViewModel[] enumerable = list as MenuViewModel[] ?? list.ToArray();
        //    return enumerable.Where(p => p.ParentId == parentId).Select(p => new MenuViewModel
        //    {
        //        MenuId = p.MenuId,
        //        MenuName = p.MenuName,
        //        MenuPath = p.MenuPath,
        //        MenuType = p.MenuType,
        //        ParentId = p.ParentId,
        //        Icon = p.Icon,
        //        Children = RecursiveModule(enumerable, p.MenuId)
        //    });
        //}
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region 登录用户验证
            //1、判断Session对象是否存在
            //if (filterContext.HttpContext.Session == null)
            //{
            //    filterContext.HttpContext.Response.Write(
            //           " <script type='text/javascript'> alert('~登录已过期，请重新登录');window.top.location='/'; </script>");
            //    filterContext.RequestContext.HttpContext.Response.End();
            //    filterContext.Result = new EmptyResult();
            //    return;
            //}
            //2、登录验证
            //if (this.CurrentUser == null)
            //{
            //    filterContext.HttpContext.Response.Write(
            //        " <script type='text/javascript'> alert('登录已过期，请重新登录'); window.top.location='/';</script>");
            //    filterContext.RequestContext.HttpContext.Response.End();
            //    filterContext.Result = new EmptyResult();
            //    return;
            //}

            #endregion

            #region 公共Get变量
            //分页页码
            object p = filterContext.HttpContext.Request.Query["page"];
            if (p == null || p.ToString() == "") { Page = 1; } else { Page = int.Parse(p.ToString()); }
            //搜索关键词
            string search = filterContext.HttpContext.Request.Query["search"];
            if (!string.IsNullOrEmpty(search)) { KeyWords = search; }
            //显示分页条数
            string size = filterContext.HttpContext.Request.Query["example_length"];
            if (!string.IsNullOrEmpty(size) && System.Text.RegularExpressions.Regex.IsMatch(size.ToString(), @"^\d+$")) { PageSize = int.Parse(size.ToString()); } else { PageSize = 10; }
            #endregion
        }
    }
}
