using VL.Common.ORM;

namespace VL.Blog.Objects.Entities
{
    public class TBlogDetailProperties
    {
        #region Properties
        public static PDMDbProperty BlogId { get; set; } = new PDMDbProperty(nameof(BlogId), "BlogId", "Id", true, PDMDataType.uniqueidentifier, 0, 0, true, null);
        public static PDMDbProperty Content { get; set; } = new PDMDbProperty(nameof(Content), "Content", "内容", false, PDMDataType.nvarchar, 0, 0, true, null);
        #endregion
    }
}
