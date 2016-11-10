using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VL.Common.Protocol.IService;
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

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // GET: /Account/Register
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
                if (result.Code == Report.CodeOfSuccess)
                {
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
    }
}