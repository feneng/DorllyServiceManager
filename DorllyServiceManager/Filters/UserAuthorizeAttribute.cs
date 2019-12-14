﻿using DorllyServiceManager.Controllers;
using DorllyServiceManager.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using DorllyService.Common.Extensions;
using DorllyService.Common.Enums;
using DorllyService.Common;

namespace DorllyServiceManager
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true,Inherited =true)]
    public class UserAuthorizeAttribute : AuthorizeAttribute,IAuthorizationFilter
    {
        #region 字段和属性
        ///// <summary>
        ///// 模块别名，可配置更改
        ///// </summary>
        //public string ModuleAlias { get; set; }
        ///// <summary>
        ///// 权限动作
        ///// </summary>
        //public string OperaAction { get; set; }
        ///// <summary>
        ///// 权限访问控制器参数
        ///// </summary>
        //private string Sign { get; set; }
        ///// <summary>
        ///// 基类实例化
        ///// </summary>
        //public BaseController baseController = new BaseController();

        public const string UserAuthenticationScheme = "UserAuthenticationScheme";

        public UserAuthorizeAttribute()
        {
            this.AuthenticationSchemes = UserAuthenticationScheme;
        }

        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {
            var authenticate = context.HttpContext.AuthenticateAsync(UserAuthenticationScheme);
            if (authenticate.Result.Succeeded || SkipUserAuthorize(context.ActionDescriptor)) return;
            var request = context.HttpContext.Request;
            if (request.IsAjaxRequest())
            {
                var msg = "未登录或登录超时，请重新登录";
                var retResult = $"{{\"status\":{ResultStatus.NotLogin},\"msg\":\"{msg}\"}}";
                var json = JsonHelper.ObjectToJSON(retResult);
                var contentResult = new ContentResult { Content = json };
                context.Result = contentResult;
                return;
            }
            var url = context.HttpContext.Content("~/Login");
            url = string.Concat(url,"?returnUrl=",request.Path);
            var redirect = new RedirectResult(url);
            context.Result = redirect;
            return;
        }

        protected virtual bool SkipUserAuthorize(ActionDescriptor actionDescriptor)
        {
            return actionDescriptor.FilterDescriptors.Where(a => a.Filter is SkipUserAuthorizeAttribute).Any();
        }

        #endregion



        /// <summary>
        /// 权限认证
        /// </summary>
        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    //1、判断模块是否对应
        //    if (string.IsNullOrEmpty(ModuleAlias))
        //    {
        //        filterContext.HttpContext.Response.Write(" <script type='text/javascript'> alert('^您没有访问该页面的权限！'); </script>");
        //        filterContext.RequestContext.HttpContext.Response.End();
        //        filterContext.Result = new EmptyResult();
        //        return;
        //    }
        //    //2、判断用户是否存在
        //    if (baseController.CurrentUser == null)
        //    {
        //        filterContext.HttpContext.Response.Write(" <script type='text/javascript'> alert('^登录已过期，请重新登录！');window.top.location='/'; </script>");
        //        filterContext.RequestContext.HttpContext.Response.End();
        //        filterContext.Result = new EmptyResult();
        //        return;
        //    }
        //    //对比变量，用于权限认证
        //    var alias = ModuleAlias;

        //    #region 配置Sign调取控制器标识
        //    Sign = filterContext.RequestContext.HttpContext.Request.QueryString["sign"];
        //    if (!string.IsNullOrEmpty(Sign))
        //    {
        //        if (("," + ModuleAlias.ToLower()).Contains("," + Sign.ToLower()))
        //        {
        //            alias = Sign;
        //            filterContext.Controller.ViewData["Sign"] = Sign;
        //        }
        //    }
        //    #endregion

        //    //3、调用下面的方法，验证是否有访问此页面的权限，查看加操作
        //    var moduleId = baseController.CurrentUser.Modules.Where(p => p.alias.ToLower() == alias.ToLower()).Select(p => p.id).FirstOrDefault();
        //    bool _blAllowed = this.IsAllowed(baseController.CurrentUser, moduleId, OperaAction);
        //    if (!_blAllowed)
        //    {
        //        filterContext.HttpContext.Response.Write(" <script type='text/javascript'> alert('您没有访问当前页面的权限！');</script>");
        //        filterContext.RequestContext.HttpContext.Response.End();
        //        filterContext.Result = new EmptyResult();
        //        return;
        //    }

        //    //4、有权限访问页面，将此页面的权限集合传给页面
        //    filterContext.Controller.ViewData["PermissionList"] = GetPermissByJson(baseController.CurrentUser, moduleId);
        //}

        ///// <summary>
        ///// 获取操作权限Json字符串，供视图JS判断使用
        ///// </summary>
        //string GetPermissByJson(Account account, int moduleId)
        //{
        //    //操作权限
        //    var _varPerListThisModule = account.Permissions.Where(p => p.moduleid == moduleId).Select(e => new { e.pervalue }).ToList();
        //    return Common.JsonConverter.Serialize(_varPerListThisModule);
        //}

        ///// <summary>
        ///// 功能描述：判断用户是否有此模块的操作权限
        ///// </summary>
        //bool IsAllowed(Account user, int moduleId, string action)
        //{
        //    //判断入口
        //    if (user == null || user.Id <= 0 || moduleId == 0 || string.IsNullOrEmpty(action)) return false;
        //    //验证权限
        //    var permission = user.Permissions.Where(p => p.moduleid == moduleId);
        //    action = action.Trim(',');
        //    if (action.IndexOf(',') > 0)
        //    {
        //        permission = permission.Where(p => action.ToLower().Contains(p.pervalue.ToLower()));
        //    }
        //    else
        //    {
        //        permission = permission.Where(p => p.pervalue.ToLower() == action.ToLower());
        //    }
        //    return permission.Any();
        //}

    }
}
