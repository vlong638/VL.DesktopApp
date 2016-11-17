using System;
using System.Collections.Generic;
using VL.Common.Object.ORM;
using VL.Common.ORM;

namespace VL.Blog.Objects.Entities
{
    public class TBlogTagMapperProperties
    {
        #region Properties
        public static PDMDbProperty TagId { get; set; } = new PDMDbProperty(nameof(TagId), "TagId", "Id", true, PDMDataType.uniqueidentifier, 0, 0, true, null);
        public static PDMDbProperty BlogId { get; set; } = new PDMDbProperty(nameof(BlogId), "BlogId", "Id", true, PDMDataType.uniqueidentifier, 0, 0, true, null);
        #endregion
    }
}
