using System.Runtime.Serialization;

namespace VL.User.Objects.Enums
{
    [DataContract]
    public enum ESignInStatus
    {
        /// <summary>
        /// 成功
        /// </summary>
        [EnumMember]
        Success = 0,
        /// <summary>
        /// 账户被锁定
        /// </summary>
        [EnumMember]
        LockedOut = 1,
        /// <summary>
        /// 需要验证
        /// </summary>
        [EnumMember]
        RequiresVerification = 2,
        /// <summary>
        /// 失败
        /// </summary>
        [EnumMember]
        Failure = 4,
    }
}
