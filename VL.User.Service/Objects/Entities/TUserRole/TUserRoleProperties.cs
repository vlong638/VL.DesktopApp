using VL.Common.ORM;

namespace VL.User.Objects.Entities
{
    public class TUserRoleProperties
    {
        #region Properties
        public static PDMDbProperty UserName { get; set; } = new PDMDbProperty(nameof(UserName), "UserName", "用户名", true, PDMDataType.nvarchar, 20, 0, true, null);
        public static PDMDbProperty RoleId { get; set; } = new PDMDbProperty(nameof(RoleId), "RoleId", "角色Id", true, PDMDataType.numeric, 16, 0, true, null);
        #endregion
    }
}
