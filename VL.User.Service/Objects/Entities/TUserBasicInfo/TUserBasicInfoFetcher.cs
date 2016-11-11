using System;
using System.Collections.Generic;
using VL.Common.DAS;
using VL.Common.ORM;
using VL.Common.Protocol;

namespace VL.User.Objects.Entities
{
    public static partial class EntityFetcher
    {
        #region Methods
        public static bool FetchTUser(this TUserBasicInfo tUserBasicInfo, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            SelectBuilder builder = new SelectBuilder();
            if (tUserBasicInfo.UserName == "")
            {
                var subselect = new SelectBuilder();
                subselect.TableName = nameof(TUserBasicInfo);
                subselect.ComponentSelect.Add(TUserBasicInfoProperties.UserName);
                subselect.ComponentWhere.Add(new ComponentValueOfWhere(TUserBasicInfoProperties.UserName, tUserBasicInfo.UserName, LocateType.Equal));
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TUserProperties.UserName, subselect, LocateType.Equal));
            }
            else
            {
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TUserProperties.UserName, tUserBasicInfo.UserName, LocateType.Equal));
            }
            query.SelectBuilders.Add(builder);
            tUserBasicInfo.User = session.GetQueryOperator().Select<TUser>(session, query);
            if (tUserBasicInfo.User == null)
            {
                throw new NotImplementedException(string.Format("1..* 关联未查询到匹配数据, Parent:{0}; Child: {1}", nameof(TUserBasicInfo), nameof(TUser)));
            }
            return true;
        }
        #endregion
    }
}
