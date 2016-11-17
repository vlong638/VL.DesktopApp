using System;
using System.Collections.Generic;
using VL.Common.Constraints.ORM;
using VL.Common.ORM;
using System.Linq;
using VL.Common.DAS;
using VL.Common.Protocol;

namespace VL.User.Objects.Entities
{
    public static partial class EntityOperator
    {
        #region Methods
        #region 写
        public static bool DbDelete(this TUser entity, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TUserProperties.UserName, entity.UserName, LocateType.Equal));
            return session.GetQueryOperator().Delete<TUser>(session, query);
        }
        public static bool DbDelete(this List<TUser> entities, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            var Ids = entities.Select(c =>c.UserName );
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TUserProperties.UserName, Ids, LocateType.In));
            return session.GetQueryOperator().Delete<TUser>(session, query);
        }
        public static bool DbInsert(this TUser entity, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            InsertBuilder builder = new InsertBuilder();
            if (entity.UserName == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.UserName));
            }
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TUserProperties.UserName, entity.UserName));
            if (entity.Password == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Password));
            }
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TUserProperties.Password, entity.Password));
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TUserProperties.CreateTime, entity.CreateTime));
            query.InsertBuilders.Add(builder);
            return session.GetQueryOperator().Insert<TUser>(session, query);
        }
        public static bool DbInsert(this List<TUser> entities, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            foreach (var entity in entities)
            {
                InsertBuilder builder = new InsertBuilder();
            if (entity.UserName == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.UserName));
            }
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TUserProperties.UserName, entity.UserName));
            if (entity.Password == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Password));
            }
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TUserProperties.Password, entity.Password));
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TUserProperties.CreateTime, entity.CreateTime));
                query.InsertBuilders.Add(builder);
            }
            return session.GetQueryOperator().InsertAll<TUser>(session, query);
        }
        public static bool DbUpdate(this TUser entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            UpdateBuilder builder = new UpdateBuilder();
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TUserProperties.UserName, entity.UserName, LocateType.Equal));
            if (fields==null|| fields.Length==0)
            {
                builder.ComponentSet.Add(new ComponentValueOfSet(TUserProperties.UserName, entity.UserName));
                builder.ComponentSet.Add(new ComponentValueOfSet(TUserProperties.Password, entity.Password));
                builder.ComponentSet.Add(new ComponentValueOfSet(TUserProperties.CreateTime, entity.CreateTime));
            }
            else
            {
                if (fields.Contains(TUserProperties.Password))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TUserProperties.Password, entity.Password));
                }
                if (fields.Contains(TUserProperties.CreateTime))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TUserProperties.CreateTime, entity.CreateTime));
                }
            }
            query.UpdateBuilders.Add(builder);
            return session.GetQueryOperator().Update<TUser>(session, query);
        }
        public static bool DbUpdate(this List<TUser> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            foreach (var entity in entities)
            {
                UpdateBuilder builder = new UpdateBuilder();
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TUserProperties.UserName, entity.UserName, LocateType.Equal));
                if (fields==null|| fields.Length==0)
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TUserProperties.UserName, entity.UserName));
                    builder.ComponentSet.Add(new ComponentValueOfSet(TUserProperties.Password, entity.Password));
                    builder.ComponentSet.Add(new ComponentValueOfSet(TUserProperties.CreateTime, entity.CreateTime));
                }
                else
                {
                    if (fields.Contains(TUserProperties.Password))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TUserProperties.Password, entity.Password));
                    }
                    if (fields.Contains(TUserProperties.CreateTime))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TUserProperties.CreateTime, entity.CreateTime));
                    }
                }
                query.UpdateBuilders.Add(builder);
            }
            return session.GetQueryOperator().UpdateAll<TUser>(session, query);
        }
        #endregion
        #region 读
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static TUser DbSelect(this TUser entity, DbSession session, SelectBuilder select)
        {
            var query = session.GetDbQueryBuilder();
            query.SelectBuilder = select;
            return session.GetQueryOperator().Select<TUser>(session, query);
        }
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static TUser DbSelect(this TUser entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentSelect.Add(TUserProperties.UserName);
                builder.ComponentSelect.Add(TUserProperties.Password);
                builder.ComponentSelect.Add(TUserProperties.CreateTime);
            }
            else
            {
                builder.ComponentSelect.Add(TUserProperties.UserName);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Add(field);
                }
            }
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TUserProperties.UserName, entity.UserName, LocateType.Equal));
            query.SelectBuilders.Add(builder);
            return session.GetQueryOperator().Select<TUser>(session, query);
        }
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static List<TUser> DbSelect(this List<TUser> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentSelect.Add(TUserProperties.UserName);
                builder.ComponentSelect.Add(TUserProperties.Password);
                builder.ComponentSelect.Add(TUserProperties.CreateTime);
            }
            else
            {
                builder.ComponentSelect.Add(TUserProperties.UserName);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Add(field);
                }
            }
            var Ids = entities.Select(c =>c.UserName );
            if (Ids.Count() != 0)
            {
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TUserProperties.UserName, Ids, LocateType.In));
            }
            query.SelectBuilders.Add(builder);
            return session.GetQueryOperator().SelectAll<TUser>(session, query);
        }
        /// <summary>
        /// 存在相应对象时返回true,缺少对象时返回false
        /// </summary>
        public static bool DbLoad(this TUser entity, DbSession session, params PDMDbProperty[] fields)
        {
            var result = entity.DbSelect(session, fields);
            if (result == null)
            {
                return false;
            }
            if (fields.Count() == 0)
            {
                entity.Password = result.Password;
                entity.CreateTime = result.CreateTime;
            }
            else
            {
                if (fields.Contains(TUserProperties.Password))
                {
                    entity.Password = result.Password;
                }
                if (fields.Contains(TUserProperties.CreateTime))
                {
                    entity.CreateTime = result.CreateTime;
                }
            }
            return true;
        }
        /// <summary>
        /// 存在相应对象时返回true,缺少对象时返回false
        /// </summary>
        public static bool DbLoad(this List<TUser> entities, DbSession session, params PDMDbProperty[] fields)
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
