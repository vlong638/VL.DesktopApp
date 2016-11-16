using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VL.ItsMe1110.Custom.Authentications;
using VL.ItsMe1110.Custom.VerficationCodes;

namespace VL.ItsMe1110.Controllers
{
    public class ValidateCodeController : Controller
    {
        #region 验证码
        [HttpGet]
        public ActionResult GetImg()
        {
            int codeLength = 4;
            int width = 100;
            int height = 40;
            int fontsize = 20;
            string code = string.Empty;
            byte[] bytes = ValidateCode.CreateValidateGraphic(out code, codeLength, width, height, fontsize);
            VLAuthentication.SetValidCodeCookie(code.ToUpper());
            //Session["code"] = code;
            return File(bytes, @"image/jpeg");
        }
        #endregion
    }
}