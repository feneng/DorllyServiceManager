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
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true,Inherited =true)]
    public class UserAuthorizeAttribute : AuthorizeAttribute,IAuthorizationFilter
    {
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
    }
}
