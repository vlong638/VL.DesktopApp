using System;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Mvc;
using VL.Common.Constraints;
using VL.ItsMe1107.Controllers;
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

        #region Login
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

            // 这不会计入到为执行帐户锁定而统计的登录失败次数中
            // 若要在多次输入错误密码的情况下触发帐户锁定，请更改为 shouldLockout: true
            var result = await SubjectUserService.AuthenticateUserAsync(new TUser() { UserName = model.UserName, Password = model.Password }
            , rememberMe: model.RememberMe
            , shouldLockout: false);
            if (result.Code == ProtocolConstraits.CodeOfSuccess)
            {
                GenericIdentity identity = new GenericIdentity(model.UserName);
                GenericPrincipal principal = new GenericPrincipal(identity, new string[] { nameof(WindowsBuiltInRole.User) });
                HttpContext.User = principal;
            }
            else
            {
                AddMessages(result.Code, new KeyValueCollection {
                        new KeyValue(4, "用户名不可为空") ,
                        new KeyValue(5, "用户不存在") ,
                        new KeyValue(6, "密码不可为空") ,
                        new KeyValue(7, "操作数据库失败") ,
                    });
            }
            return View(model);
        }
        #endregion

        #region Register
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
            if (ModelState.IsValid)
            {
                var user = new TUser()
                {
                    UserName = model.UserName,
                    Password = model.Password,
                    CreateTime = DateTime.Now,
                };
                var result = await SubjectUserService.CreateUserAsync(user);
                if (result.Code == ProtocolConstraits.CodeOfSuccess)
                {
                    GenericIdentity identity = new GenericIdentity(user.UserName);
                    GenericPrincipal principal = new GenericPrincipal(identity, new string[] { nameof(WindowsBuiltInRole.User) });
                    HttpContext.User = principal;
                    return RedirectToAction(nameof(HomeController.Index), HomeController.PageName_Home);
                }
                else
                {
                    AddMessages(result.Code, new KeyValueCollection {
                        new KeyValue(4, "用户名不可为空") ,
                        new KeyValue(5, "用户已存在") ,
                        new KeyValue(6, "密码不可为空") ,
                        new KeyValue(7, "操作数据库失败") ,
                    });
                }
            }
            return View(model);
        } 
        #endregion
    }
}