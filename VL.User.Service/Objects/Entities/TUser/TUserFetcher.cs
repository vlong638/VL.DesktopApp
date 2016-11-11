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
        public static bool FetchTUserBasicInfo(this TUser tUser, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            SelectBuilder builder = new SelectBuilder();
            if (tUser.UserName == "")
            {
                var subselect = new SelectBuilder();
                subselect.TableName = nameof(TUser);
                subselect.ComponentSelect.Add(TUserProperties.UserName);
                subselect.ComponentWhere.Add(new ComponentValueOfWhere(TUserProperties.UserName, tUser.UserName, LocateType.Equal));
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TUserBasicInfoProperties.UserName, subselect, LocateType.Equal));
            }
            else
            {
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TUserBasicInfoProperties.UserName, tUser.UserName, LocateType.Equal));
            }
            query.SelectBuilders.Add(builder);
            tUser.UserBasicInfo = session.GetQueryOperator().Select<TUserBasicInfo>(session, query);
            if (tUser.UserBasicInfo == null)
            {
                throw new NotImplementedException(string.Format("1..* 关联未查询到匹配数据, Parent:{0}; Child: {1}", nameof(TUser), nameof(TUserBasicInfo)));
            }
            return true;
        }
        #endregion
    }
}
