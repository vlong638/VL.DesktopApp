using VL.Common.Core.DAS;
using VL.Common.Core.Object.VL.Blog;
using VL.Common.Core.Protocol;

namespace VL.Blog.Business
{
    public static class TBlogTagMapperDomain
    {
        public static bool IsBlogHasTags(this TBlogTagMapper blogTag, DbSession session)
        {
            var query = session.GetDbQueryBuilder().SelectBuilder;
            query.ComponentSelect.Add("1");
            query.ComponentWhere.Add(TBlogTagMapperProperties.TagName == blogTag.TagName);
            return session.GetQueryOperator().SelectAsInt<TBlogTagMapper>(query) != null;
        }
    }
}
