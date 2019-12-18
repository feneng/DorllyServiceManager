using System;
using System.Linq;
using DorllyService.Common;
using DorllyService.Common.Enums;
using DorllyService.Common.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DorllyServiceManager
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AdminAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public const string AdminAuthenticationScheme = "AdminAuthenticationScheme";
        public AdminAuthorizeAttribute()
        {
            this.AuthenticationSchemes = AdminAuthenticationScheme;
        }

        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {
            var authenticate = context.HttpContext.AuthenticateAsync(AdminAuthenticationScheme);
            authenticate.Wait();
            if (authenticate.Result.Succeeded || SkipAdminAuthorize(context.ActionDescriptor))
                return;
            var request = context.HttpContext.Request;
            if (request.IsAjaxRequest())
            {
                var msg = "未登录或登录超时，请重新登录";
                var result = $"{{\"status\":{ResultStatus.NotLogin},\"msg\":\"{msg}\"}}";
                var json = JsonHelper.ObjectToJSON(result);
                context.Result = new ContentResult { Content = json };
                return;
            }
            var url = context.HttpContext.Content("~/Admin/Login");
            url = string.Concat(url, "?returnUrl=", request.Path);
            var redirect = new RedirectResult(url);
            context.Result = redirect;
            return;
        }

        protected virtual bool SkipAdminAuthorize(ActionDescriptor actionDescriptor)
        {
            return actionDescriptor.FilterDescriptors.Any(a => a.Filter is SkipAdminAuthorizeAttribute);
        }
    }
}
