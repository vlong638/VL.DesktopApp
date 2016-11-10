using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VL.Common.Protocol.IService;

namespace VL.ItsMe1110.Controllers
{
    public abstract class BaseController : Controller
    {
        public const string PageName_Error = "Error";
        public const string PageName_Lockout = "Lockout";
        public const string PageName_Home = "Home";
        public const string PageName_Account = "Account";

        #region 辅助方法
        protected class KeyValue
        {
            public KeyValue(int key, params string[] values)
            {
                Key = key;
                Values = values;
            }

            public int Key{ set; get; }
            public string[] Values{ set; get; }
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
            if (details==null)
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
    }
}