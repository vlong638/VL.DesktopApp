using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VL.ItsMe1110.Custom.Authentications
{
    public class VLAuthorizeAttribute:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = httpContext.User as VLPrincipal;
            if (user != null)
                return user.IsInRole(Roles) || user.IsInUser(Users);
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("http://www.baidu.com");
        }
    }
}