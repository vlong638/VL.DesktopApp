using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using VL.Common.Object.ORM;
using VL.Common.ORM;

namespace VL.Blog.Objects.Enums
{
    [DataContract]
    public enum ESample
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
