using System;
using System.Collections.Generic;
using System.Linq;
using VL.Common.DAS.Objects;
using VL.Common.ORM.Objects;
using VL.Common.ORM.Utilities.QueryBuilders;
using VL.Common.Protocol.IService;

namespace VL.User.Objects.Entities
{
    public static partial class EntityOperator
    {
        #region Methods
        #region 写
        public static bool DbDelete(this TUser entity, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TUserProperties.UserName, entity.UserName, LocateType.Equal));
            return IORMProvider.GetQueryOperator(session).Delete<TUser>(session, query);
        }
        public static bool DbDelete(this List<TUser> entities, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            var Ids = entities.Select(c =>c.UserName );
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TUserProperties.UserName, Ids, LocateType.In));
            return IORMProvider.GetQueryOperator(session).Delete<TUser>(session, query);
        }
        public static bool DbInsert(this TUser entity, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
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
            return IORMProvider.GetQueryOperator(session).Insert<TUser>(session, query);
        }
        public static bool DbInsert(this List<TUser> entities, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
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
            return IORMProvider.GetQueryOperator(session).InsertAll<TUser>(session, query);
        }
        public static bool DbUpdate(this TUser entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
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
            return IORMProvider.GetQueryOperator(session).Update<TUser>(session, query);
        }
        public static bool DbUpdate(this List<TUser> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
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
            return IORMProvider.GetQueryOperator(session).UpdateAll<TUser>(session, query);
        }
        #endregion
        #region 读
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static TUser DbSelect(this TUser entity, DbSession session, SelectBuilder select)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            query.SelectBuilder = select;
            return IORMProvider.GetQueryOperator(session).Select<TUser>(session, query);
        }
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static TUser DbSelect(this TUser entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
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
            return IORMProvider.GetQueryOperator(session).Select<TUser>(session, query);
        }
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static List<TUser> DbSelect(this List<TUser> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
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
            return IORMProvider.GetQueryOperator(session).SelectAll<TUser>(session, query);
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
