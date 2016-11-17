using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using VL.Common.Object.Protocol;
using VL.Common.Object.VL.User;
using VL.ItsMe1110.Custom.Authentications;
using VL.ItsMe1110.Models;
using VL.ItsMe1110.SubjectUserService;

namespace VL.ItsMe1110.Controllers
{
    public class AccountController : BaseController
    {
        private SubjectUserServiceClient _subjectUserService;
        public SubjectUserServiceClient SubjectUserService
        {
            get
            {
                if (_subjectUserService == null)
                {
                    _subjectUserService = new SubjectUserServiceClient();
                }
                return _subjectUserService;
            }
        }

        #region 登录
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //验证码
            var validCode = VLAuthentication.TryParseValidCode(HttpContext.Request.Cookies);
            if (validCode != model.ValidateCode.ToUpper())
            {
                ModelState.AddModelError("", "验证码错误。");
                return View(model);
            }

            // 这不会计入到为执行帐户锁定而统计的登录失败次数中
            // 若要在多次输入错误密码的情况下触发帐户锁定，请更改为 shouldLockout: true
            var user = new TUser() { UserName = model.UserName, Password = model.Password };
            var result = await SubjectUserService.AuthenticateUserAsync(user,
                rememberMe: model.RememberMe,
                shouldLockout: false);
            if (result.Code == CProtocol.CReport.CSuccess)
            {
                #region 博客园
                ////博客园
                //HttpCookie authCookie = FormsAuthentication.GetAuthCookie(model.UserName, model.RememberMe);
                //FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                //FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, userDataString);
                //authCookie.Value = FormsAuthentication.Encrypt(newTicket);
                //Response.Cookies.Add(authCookie);
                //string redirUrl = FormsAuthentication.GetRedirectUrl(userName.Text, RememberMe.Checked);
                //Response.Redirect(redirUrl); 
                #endregion

                #region 微软官方
                ////微软官方
                //string userData = "ApplicationSpecific data for this user.";
                //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                //  model.UserName,
                //  DateTime.Now,
                //  DateTime.Now.AddMinutes(30),
                //  model.RememberMe,
                //  userData,
                //  FormsAuthentication.FormsCookiePath);
                //string encTicket = FormsAuthentication.Encrypt(ticket);
                //Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                //return RedirectToLocal(returnUrl); 
                #endregion

                VLAuthentication.SetAuthCookie(user, model.RememberMe);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                switch (result.Data)
                {
                    case ESignInStatus.Success:
                        ModelState.AddModelError("", "错误导向。");
                        return View(model);
                    case ESignInStatus.LockedOut:
                        return View(PageName_Lockout);
                    case ESignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    case ESignInStatus.Failure:
                    default:
                        AddMessages(result.Code, new KeyValueCollection {
                            new KeyValue(11, "用户名不可为空") ,
                            new KeyValue(12, "用户不存在") ,
                            new KeyValue(13, "密码不可为空") ,
                            new KeyValue(14, "操作数据库失败") ,
                        });
                        return View(model);
                }
            }
        }
        #endregion

        #region 注册
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //验证码
            var validCode = VLAuthentication.TryParseValidCode(HttpContext.Request.Cookies);
            if (validCode != model.ValidateCode.ToUpper())
            {
                ModelState.AddModelError("", "验证码错误。");
                return View(model);
            }

            var user = new TUser()
            {
                UserName = model.UserName,
                Password = model.Password,
                CreateTime = DateTime.Now,
            };
            var result = await SubjectUserService.CreateUserAsync(user);
            if (result.Code == CProtocol.CReport.CSuccess)
            {
                VLAuthentication.SetAuthCookie(user, false);
                return RedirectToAction(nameof(HomeController.Index), HomeController.PageName_Home);
            }
            else
            {
                AddMessages(result.Code, new KeyValueCollection {
                        new KeyValue(11, "用户名不可为空") ,
                        new KeyValue(12, "用户已存在") ,
                        new KeyValue(13, "密码不可为空") ,
                        new KeyValue(14, "操作数据库失败") ,
                    });
            }
            return View(model);
        }
        #endregion

        #region 注销
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            VLAuthentication.LogOff();
            return RedirectToAction(nameof(HomeController.Index), PageName_Home);
        }
        #endregion

        #region 邮箱验证
        #endregion
    }
}