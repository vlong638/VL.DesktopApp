using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VL.ItsMe1110.Attributes
{
    /// <summary>
    /// 用于测试AuthorizeAttribute
    /// [CustomAuth(false)]
    /// 拦截本地请求,一般不需要自定制
    /// MVC标准实现为
    /// [Authorize(Order = -1,Roles = "", Users = "")]
    /// </summary>
    public class CustomAuthAttribute: AuthorizeAttribute
    {
        private bool localAllowed;
        public CustomAuthAttribute(bool allowedParam)
        {
            localAllowed = allowedParam;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Request.IsLocal)
            {
                return localAllowed;
            }
            else
            {
                return true;
            }
        }
    }
}