using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VL.ItsMe1110.Custom.Authentications;

namespace VL.ItsMe1110.Controllers
{
    public class BlogController : Controller
    {
        [HttpGet]
        [VLAuthorize(Users ="vlong638")]
        public ActionResult Index()
        {
            return View();
        }
    }
}