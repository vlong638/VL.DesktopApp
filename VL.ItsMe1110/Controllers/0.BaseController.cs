using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace VL.ItsMe1110.Controllers
{
    public abstract class BaseController : Controller
    {
        public const string PageName_Error = "Error";
        public const string PageName_Lockout = "Lockout";

        public const string PageName_Account = "Account";
        public const string PageName_Blog = "Blog";
        public const string PageName_Home = "Home";

        public static List<string> VestedUsers = VestedUserString.Split(',').ToList();
        public const string VestedUserString = "vlong638,yyt";


        #region 辅助方法

        #region 页面跳转
        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(HomeController.Index), PageName_Home);
        }
        #endregion

        #region 输出报告
        protected class KeyValue
        {
            public KeyValue(int key, params string[] values)
            {
                Key = key;
                Values = values;
            }

            public int Key { set; get; }
            public string[] Values { set; get; }
        }
        protected class KeyValueCollection : List<KeyValue>
        {
            public bool ContainsKey(int key)
            {
                return this.FirstOrDefault(c => c.Key == key) != null;
            }

        }
        /// <summary>
        /// 将Report的报告输出到页面
        /// </summary>
        /// <param name="result"></param>
        protected void AddMessages(int code, KeyValueCollection codeDetails)
        {
            var details = codeDetails.FirstOrDefault(c => c.Key == code);
            if (details == null)
            {
                ModelState.AddModelError("", "未配置对应Code的详情报告,Code:" + code.ToString());
            }
            else
            {
                foreach (var detail in details.Values)
                {
                    ModelState.AddModelError("", detail);
                }
            }
        } 
        #endregion

        #endregion
    }
}