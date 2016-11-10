using System.Runtime.Serialization;

namespace VL.User.Objects.Enums
{
    [DataContract]
    public enum EGender
    {
        /// <summary>
        /// 未指定
        /// </summary>
        [EnumMember]
        None = 0,
        /// <summary>
        /// 男
        /// </summary>
        [EnumMember]
        Man = 1,
        /// <summary>
        /// 女
        /// </summary>
        [EnumMember]
        Woman = 2,
    }
}
