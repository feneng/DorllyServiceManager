using System;
using Microsoft.AspNetCore.Http;

namespace DorllyService.Common.Extensions
{
    public static class ExtendHttpRequest
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request==null)
            {
                throw new ArgumentNullException("null request");
            }

            return (request.Headers["X-Requested-With"] == "XMLHttpRequest") ||
                (request.Headers != null) && (request.Headers["X-Requested"] == "XMLHttpRequest");
        }
    }
}
