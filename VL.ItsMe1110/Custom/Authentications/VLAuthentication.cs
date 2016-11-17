using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using VL.Common.Object.VL.User;
using VL.ItsMe1110.Custom.VerficationCodes;
using VL.ItsMe1110.SubjectUserService;

namespace VL.ItsMe1110.Custom.Authentications
{
    /// <summary>
    /// 验证辅助,用于处理Cookie
    /// </summary>
    public class VLAuthentication
    {
        #region 用户验证
        private static int AuthCookie_SaveDays = 14;
        private static string AuthCookie_Name = FormsAuthentication.FormsCookieName;

        /// <summary>
        /// 将用户信息写入Cookie
        /// </summary>
        public static void SetAuthCookie(TUser user, bool rememberMe)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            //Ticket
            var ticket = new FormsAuthenticationTicket(1,
                user.UserName,
                DateTime.Now,
                DateTime.Now.AddDays(AuthCookie_SaveDays),
                rememberMe,
                Newtonsoft.Json.JsonConvert.SerializeObject(user),
                FormsAuthentication.FormsCookiePath);
            //加密
            string enTicket = FormsAuthentication.Encrypt(ticket);
            //Cookie
            var cookie = new HttpCookie(AuthCookie_Name, enTicket)
            {
                HttpOnly = true,//防止脚本访问
                Secure = FormsAuthentication.RequireSSL,
                Domain = FormsAuthentication.CookieDomain,
                Path = FormsAuthentication.FormsCookiePath,
            };
            if (rememberMe)
            {
                cookie.Expires = DateTime.Now.AddDays(AuthCookie_SaveDays);
            }

            //写入Response
            HttpContext.Current.Response.Cookies.Remove(cookie.Name);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        /// <summary>
        /// 从Request的Cookie读取Principal
        /// </summary>
        public static VLPrincipal TryParsePrincipal(HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var cookie = request.Cookies[AuthCookie_Name];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
                return null;
            try
            {
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                if (ticket != null && !string.IsNullOrEmpty(ticket.UserData))
                {
                    var user = Newtonsoft.Json.JsonConvert.DeserializeObject<TUser>(ticket.UserData);
                    if (user != null)
                    {
                        return new VLPrincipal(ticket, user);
                    }
                }
            }
            catch// (Exception ex)
            {
                //TODO Log it?
            }
            return null;
        }
        /// <summary>
        /// 清空Cookie
        /// </summary>
        public static void LogOff()
        {
            FormsAuthentication.SignOut();
        }
        #endregion

        #region 验证码
        private const int ValidCodeCookie_SaveMinutes = 3;
        private static string ValidCodeCookie_Name = "_CODEVERIFICATION";

        /// <summary>
        /// 将用户信息写入Cookie
        /// </summary>
        public static void SetValidCodeCookie(string validCode)
        {
            if (validCode == null)
                throw new ArgumentNullException(nameof(validCode));

            //Cookie
            var cookie = new HttpCookie(ValidCodeCookie_Name,MD5Helper.Encode(validCode))
            {
                HttpOnly = true,//防止脚本访问
                Secure = FormsAuthentication.RequireSSL,
                Domain = FormsAuthentication.CookieDomain,
                Path = FormsAuthentication.FormsCookiePath,
                Expires= DateTime.Now.AddMinutes(ValidCodeCookie_SaveMinutes),
            };
            //写入Response
            HttpContext.Current.Response.Cookies.Remove(cookie.Name);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        /// <summary>
        /// 从Request的Cookie读取Principal
        /// </summary>
        public static string TryParseValidCode(HttpCookieCollection cookies)
        {
            if (cookies == null)
            {
                throw new ArgumentNullException(nameof(cookies));
            }

            var cookie = cookies[ValidCodeCookie_Name];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
                return null;
            try
            {
                return MD5Helper.Decode(cookie.Value);
            }
            catch// (Exception ex)
            {
                //TODO Log it?
            }
            return null;
        }
        #endregion
    }
}