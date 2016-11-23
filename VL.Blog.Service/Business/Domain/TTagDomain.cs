using System;
using VL.Common.Core.DAS;
using VL.Common.Core.ORM;
using VL.Common.Core.Protocol;
using VL.Common.Object.VL.Blog;

namespace VL.Blog.Business
{
    public static class TTagDomain
    {
        public static bool CheckExistance(this TTag tag,DbSession session, string userName,string tagName)
        {
            var query = session.GetDbQueryBuilder().SelectBuilder;
            query.ComponentSelect.Add("1");
            query.ComponentWhere.Add(TTagProperties.UserName, userName, LocateType.Equal);
            throw new NotImplementedException();
        }
    }
}
