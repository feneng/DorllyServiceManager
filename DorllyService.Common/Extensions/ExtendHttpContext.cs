using System.Linq;
using Microsoft.AspNetCore.Http;

namespace DorllyService.Common.Extensions
{
    public static class ExtendHttpContext
    {
        public static string Content(this HttpContext httpContext, string contentPath)
        {
            if (string.IsNullOrEmpty(contentPath))
            {
                return null;
            }
            if (contentPath[0] == '~')
            {
                var segment = new PathString(contentPath.Substring(1));
                var applicationPath = httpContext.Request.PathBase;
                return applicationPath.Add(segment).Value;
            }
            return contentPath;
        }

        public static bool IsAjaxRequest(this HttpContext httpContext)
        {
            return httpContext.Request.IsAjaxRequest();
        }

        public static string GetClientIP(this HttpContext httpContext)
        {
            var ip = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = httpContext.Connection.RemoteIpAddress.ToString();
            }

            if (ip == "::1") ip = "127.0.0.1";
            return ip;
        }

        public static string GetRequestUrl(this HttpContext httpContext)
        {
            var httpRequest = httpContext.Request;

            var scheme = httpRequest.Scheme;
            var host = httpRequest.Host.Value;
            var path = httpRequest.Path.Value;
            var queryString = httpRequest.QueryString.Value;

            return $"{scheme}://{host}{path}{queryString}";
        }

        public static string GetUrlReferer(this HttpContext httpContext)
        {
            return httpContext.Request.Headers["Referer"];
        }
    }
}
