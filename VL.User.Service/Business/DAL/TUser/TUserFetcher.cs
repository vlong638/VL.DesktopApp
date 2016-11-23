using System;
using System.Collections.Generic;
using VL.Common.Core.DAS;
using VL.Common.Core.ORM;
using VL.Common.Core.Protocol;
using VL.Common.Object.VL.User;

namespace VL.User.Business
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
            tUser.UserBasicInfo = session.GetQueryOperator().Select<TUserBasicInfo>(query);
            if (tUser.UserBasicInfo == null)
            {
                throw new NotImplementedException(string.Format("1..* 关联未查询到匹配数据, Parent:{0}; Child: {1}", nameof(TUser), nameof(TUserBasicInfo)));
            }
            return true;
        }
        public static bool FetchUserRoles(this TUser tUser, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            SelectBuilder builder = new SelectBuilder();
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TUserRoleProperties.UserName, tUser.UserName, LocateType.Equal));
            query.SelectBuilders.Add(builder);
            tUser.UserRoles = session.GetQueryOperator().SelectAll<TUserRole>(query);
            return tUser.UserRoles.Count > 0;
        }
        #endregion
    }
}
