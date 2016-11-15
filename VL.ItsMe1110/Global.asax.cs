using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using VL.ItsMe1110.Custom.Authentications;

namespace VL.ItsMe1110
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_PostAuthenticateRequest(object sender, System.EventArgs e)
        {




            var formsIdentity = HttpContext.Current.User.Identity as FormsIdentity;
            if (formsIdentity != null && formsIdentity.IsAuthenticated && formsIdentity.AuthenticationType == "Forms")
            {
                HttpContext.Current.User = VLAuthentication.TryParsePrincipal(HttpContext.Current.Request);
            }
        }
    }
}
