using System;
using System.Collections.Generic;
using System.Linq;
using VL.Common.Core.DAS;
using VL.Common.Core.ORM;
using VL.Common.Core.Protocol;
using VL.Common.Object.VL.User;

namespace VL.User.Business
{
    public static partial class EntityOperator
    {
        #region Methods
        #region 写
        public static bool DbDelete(this TUserRole entity, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TUserRoleProperties.UserName, entity.UserName, LocateType.Equal));
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TUserRoleProperties.RoleId, entity.RoleId, LocateType.Equal));
            return session.GetQueryOperator().Delete<TUserRole>(query);
        }
        public static bool DbDelete(this List<TUserRole> entities, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            var Ids = entities.Select(c =>c.UserName );
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TUserRoleProperties.UserName, Ids, LocateType.In));
            return session.GetQueryOperator().Delete<TUserRole>(query);
        }
        public static bool DbInsert(this TUserRole entity, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            InsertBuilder builder = new InsertBuilder();
            if (entity.UserName == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.UserName));
            }
            if (entity.UserName.Length > 20)
            {
                throw new NotImplementedException(string.Format("参数项:{0}长度:{1}超过额定限制:{2}", nameof(entity.UserName), entity.UserName.Length, 20));
            }
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TUserRoleProperties.UserName, entity.UserName));
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TUserRoleProperties.RoleId, entity.RoleId));
            query.InsertBuilders.Add(builder);
            return session.GetQueryOperator().Insert<TUserRole>(query);
        }
        public static bool DbInsert(this List<TUserRole> entities, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            foreach (var entity in entities)
            {
                InsertBuilder builder = new InsertBuilder();
            if (entity.UserName == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.UserName));
            }
            if (entity.UserName.Length > 20)
            {
                throw new NotImplementedException(string.Format("参数项:{0}长度:{1}超过额定限制:{2}", nameof(entity.UserName), entity.UserName.Length, 20));
            }
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TUserRoleProperties.UserName, entity.UserName));
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TUserRoleProperties.RoleId, entity.RoleId));
                query.InsertBuilders.Add(builder);
            }
            return session.GetQueryOperator().InsertAll<TUserRole>(query);
        }
        public static bool DbUpdate(this TUserRole entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            UpdateBuilder builder = new UpdateBuilder();
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TUserRoleProperties.UserName, entity.UserName, LocateType.Equal));
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TUserRoleProperties.RoleId, entity.RoleId, LocateType.Equal));
            if (fields==null|| fields.Length==0)
            {
            }
            else
            {
            }
            query.UpdateBuilders.Add(builder);
            return session.GetQueryOperator().Update<TUserRole>(query);
        }
        public static bool DbUpdate(this List<TUserRole> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            foreach (var entity in entities)
            {
                UpdateBuilder builder = new UpdateBuilder();
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TUserRoleProperties.UserName, entity.UserName, LocateType.Equal));
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TUserRoleProperties.RoleId, entity.RoleId, LocateType.Equal));
                if (fields==null|| fields.Length==0)
                {
                }
                else
                {
                }
                query.UpdateBuilders.Add(builder);
            }
            return session.GetQueryOperator().UpdateAll<TUserRole>(query);
        }
        #endregion
        #region 读
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static TUserRole DbSelect(this TUserRole entity, DbSession session, SelectBuilder select)
        {
            var query = session.GetDbQueryBuilder();
            query.SelectBuilder = select;
            return session.GetQueryOperator().Select<TUserRole>(query);
        }
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static TUserRole DbSelect(this TUserRole entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentSelect.Add(TUserRoleProperties.UserName);
                builder.ComponentSelect.Add(TUserRoleProperties.RoleId);
            }
            else
            {
                builder.ComponentSelect.Add(TUserRoleProperties.UserName);
                builder.ComponentSelect.Add(TUserRoleProperties.RoleId);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Add(field);
                }
            }
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TUserRoleProperties.UserName, entity.UserName, LocateType.Equal));
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TUserRoleProperties.RoleId, entity.RoleId, LocateType.Equal));
            query.SelectBuilders.Add(builder);
            return session.GetQueryOperator().Select<TUserRole>(query);
        }
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static List<TUserRole> DbSelect(this List<TUserRole> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentSelect.Add(TUserRoleProperties.UserName);
                builder.ComponentSelect.Add(TUserRoleProperties.RoleId);
            }
            else
            {
                builder.ComponentSelect.Add(TUserRoleProperties.UserName);
                builder.ComponentSelect.Add(TUserRoleProperties.RoleId);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Add(field);
                }
            }
            var Ids = entities.Select(c =>c.UserName );
            if (Ids.Count() != 0)
            {
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TUserRoleProperties.UserName, Ids, LocateType.In));
            }
            query.SelectBuilders.Add(builder);
            return session.GetQueryOperator().SelectAll<TUserRole>(query);
        }
        /// <summary>
        /// 存在相应对象时返回true,缺少对象时返回false
        /// </summary>
        public static bool DbLoad(this TUserRole entity, DbSession session, params PDMDbProperty[] fields)
        {
            var result = entity.DbSelect(session, fields);
            if (result == null)
            {
                return false;
            }
            if (fields.Count() == 0)
            {
            }
            else
            {
            }
            return true;
        }
        /// <summary>
        /// 存在相应对象时返回true,缺少对象时返回false
        /// </summary>
        public static bool DbLoad(this List<TUserRole> entities, DbSession session, params PDMDbProperty[] fields)
        {
            bool result = true;
            foreach (var entity in entities)
            {
                result = result && entity.DbLoad(session, fields);
            }
            return result;
        }
        #endregion
        #endregion
    }
}
