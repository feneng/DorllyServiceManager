using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DorllyServiceManager
{
    /// <summary>
    /// 跳过验证检查
    /// </summary>
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple =false,Inherited =true)]
    public class SkipUserAuthorizeAttribute:Attribute,IFilterMetadata
    {
    }
}
