using System;
using System.Collections.Generic;
using VL.Common.Object.ORM;
using VL.Common.ORM;

namespace VL.Blog.Objects.Entities
{
    public class TTagProperties
    {
        #region Properties
        public static PDMDbProperty UserName { get; set; } = new PDMDbProperty(nameof(UserName), "UserName", "用户名", false, PDMDataType.nvarchar, 20, 0, true, null);
        public static PDMDbProperty TagId { get; set; } = new PDMDbProperty(nameof(TagId), "TagId", "Id", true, PDMDataType.uniqueidentifier, 0, 0, true, null);
        public static PDMDbProperty Name { get; set; } = new PDMDbProperty(nameof(Name), "Name", "标签名", false, PDMDataType.nvarchar, 20, 0, true, null);
        #endregion
    }
}
