using System;
using System.Collections.Generic;
using VL.Common.Core.DAS;
using VL.Common.Core.ORM;
using VL.Common.Core.Protocol;
using VL.Common.Core.Object.VL.Blog;

namespace VL.Blog.Business
{
    public static partial class EntityFetcher
    {
        #region Methods
        public static bool FetchBlogTagMappers(this TBlog tBlog, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            SelectBuilder builder = new SelectBuilder();
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogTagMapperProperties.BlogId, tBlog.BlogId, LocateType.Equal));
            query.SelectBuilders.Add(builder);
            tBlog.BlogTagMappers = session.GetQueryOperator().SelectAll<TBlogTagMapper>(query);
            return tBlog.BlogTagMappers.Count > 0;
        }
        #endregion
    }
}
