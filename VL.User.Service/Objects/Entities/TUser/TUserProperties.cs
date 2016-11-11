using VL.Common.ORM;

namespace VL.User.Objects.Entities
{
    public class TUserProperties
    {
        #region Properties
        public static PDMDbProperty UserName { get; set; } = new PDMDbProperty(nameof(UserName), "UserName", "用户名", true, PDMDataType.nvarchar, 20, 0, true, null);
        public static PDMDbProperty Password { get; set; } = new PDMDbProperty(nameof(Password), "Password", "支付密码", false, PDMDataType.nvarchar, 16, 0, true, null);
        public static PDMDbProperty CreateTime { get; set; } = new PDMDbProperty(nameof(CreateTime), "CreateTime", "创建时间", false, PDMDataType.datetime, 0, 0, true, null);
        #endregion
    }
}
