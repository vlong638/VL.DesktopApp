using System;
using System.Collections.Generic;
using VL.Common.Constraints.ORM;
using VL.Common.ORM;

namespace VL.User.Objects.Entities
{
    public class TUserBasicInfoProperties
    {
        #region Properties
        public static PDMDbProperty UserName { get; set; } = new PDMDbProperty(nameof(UserName), "UserName", "用户名", true, PDMDataType.nvarchar, 20, 0, true, null);
        public static PDMDbProperty Birthday { get; set; } = new PDMDbProperty(nameof(Birthday), "Birthday", "出生日期", false, PDMDataType.datetime, 0, 0, false, null);
        public static PDMDbProperty Mobile { get; set; } = new PDMDbProperty(nameof(Mobile), "Mobile", "手机", false, PDMDataType.numeric, 11, 0, false, null);
        public static PDMDbProperty Email { get; set; } = new PDMDbProperty(nameof(Email), "Email", "电子邮箱(不区分大小写)", false, PDMDataType.nvarchar, 32, 0, false, null);
        public static PDMDbProperty IdCardNumber { get; set; } = new PDMDbProperty(nameof(IdCardNumber), "IdCardNumber", "身份证号码", false, PDMDataType.nvarchar, 18, 0, false, null);
        #endregion
    }
}
