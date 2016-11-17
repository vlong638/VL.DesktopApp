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
            {
                if (!string.IsNullOrEmpty(Users)&& user.IsInUser(Users.Split(',').ToList()))
                {
                    return true;
                }
                if (!string.IsNullOrEmpty(Roles) && user.IsInRole(Roles))
                {
                    return true;
                }
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("http://www.baidu.com");
        }
    }
}