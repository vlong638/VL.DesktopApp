using System;
using VL.Common.Core.DAS;
using VL.Common.Core.ORM;
using VL.Common.Core.Protocol;
using VL.Common.Core.Object.VL.Blog;

namespace VL.Blog.Business
{
    public static class TTagDomain
    {
        //public static bool LoadByName(this TTag tag,DbSession session)
        //{
        //    var query = session.GetDbQueryBuilder().SelectBuilder;
        //    query.ComponentSelect.Add("1");
        //    query.ComponentWhere.Add(TTagProperties.UserName, tag.UserName, LocateType.Equal);
        //    query.ComponentWhere.Add(TTagProperties.TagName, tag.TagName, LocateType.Equal);
        //    return session.GetQueryOperator().SelectAsInt<TTag>(query) != null;
        //}
    }
}
