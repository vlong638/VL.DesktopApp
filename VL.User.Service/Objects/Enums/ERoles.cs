using System.Runtime.Serialization;

namespace VL.User.Objects.Enums
{
    [DataContract]
    public enum ERoles
    {
        /// <summary>
        /// 未指定
        /// </summary>
        [EnumMember]
        None = 0,
        /// <summary>
        /// 管理员
        /// </summary>
        [EnumMember]
        Administrator = 1,
        /// <summary>
        /// 用户
        /// </summary>
        [EnumMember]
        User = 2,
        /// <summary>
        /// 游客
        /// </summary>
        [EnumMember]
        Guest = 3,
    }
}
