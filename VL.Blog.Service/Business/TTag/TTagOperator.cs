using System;
using System.Collections.Generic;
using System.Linq;
using VL.Common.DAS;
using VL.Common.Object.VL.Blog;
using VL.Common.Object.ORM;
using VL.Common.ORM;
using VL.Common.Protocol;

namespace VL.Blog.Business
{
    public static partial class EntityOperator
    {
        #region Methods
        #region 写
        public static bool DbDelete(this TTag entity, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TTagProperties.TagId, entity.TagId, LocateType.Equal));
            return session.GetQueryOperator().Delete<TTag>(session, query);
        }
        public static bool DbDelete(this List<TTag> entities, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            var Ids = entities.Select(c =>c.TagId );
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TTagProperties.TagId, Ids, LocateType.In));
            return session.GetQueryOperator().Delete<TTag>(session, query);
        }
        public static bool DbInsert(this TTag entity, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            InsertBuilder builder = new InsertBuilder();
            if (entity.UserName == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.UserName));
            }
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TTagProperties.UserName, entity.UserName));
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TTagProperties.TagId, entity.TagId));
            if (entity.Name == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Name));
            }
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TTagProperties.Name, entity.Name));
            query.InsertBuilders.Add(builder);
            return session.GetQueryOperator().Insert<TTag>(session, query);
        }
        public static bool DbInsert(this List<TTag> entities, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            foreach (var entity in entities)
            {
                InsertBuilder builder = new InsertBuilder();
            if (entity.UserName == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.UserName));
            }
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TTagProperties.UserName, entity.UserName));
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TTagProperties.TagId, entity.TagId));
            if (entity.Name == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Name));
            }
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TTagProperties.Name, entity.Name));
                query.InsertBuilders.Add(builder);
            }
            return session.GetQueryOperator().InsertAll<TTag>(session, query);
        }
        public static bool DbUpdate(this TTag entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            UpdateBuilder builder = new UpdateBuilder();
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TTagProperties.TagId, entity.TagId, LocateType.Equal));
            if (fields==null|| fields.Length==0)
            {
                builder.ComponentSet.Add(new ComponentValueOfSet(TTagProperties.UserName, entity.UserName));
                builder.ComponentSet.Add(new ComponentValueOfSet(TTagProperties.TagId, entity.TagId));
                builder.ComponentSet.Add(new ComponentValueOfSet(TTagProperties.Name, entity.Name));
            }
            else
            {
                if (fields.Contains(TTagProperties.UserName))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TTagProperties.UserName, entity.UserName));
                }
                if (fields.Contains(TTagProperties.Name))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TTagProperties.Name, entity.Name));
                }
            }
            query.UpdateBuilders.Add(builder);
            return session.GetQueryOperator().Update<TTag>(session, query);
        }
        public static bool DbUpdate(this List<TTag> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            foreach (var entity in entities)
            {
                UpdateBuilder builder = new UpdateBuilder();
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TTagProperties.TagId, entity.TagId, LocateType.Equal));
                if (fields==null|| fields.Length==0)
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TTagProperties.UserName, entity.UserName));
                    builder.ComponentSet.Add(new ComponentValueOfSet(TTagProperties.TagId, entity.TagId));
                    builder.ComponentSet.Add(new ComponentValueOfSet(TTagProperties.Name, entity.Name));
                }
                else
                {
                    if (fields.Contains(TTagProperties.UserName))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TTagProperties.UserName, entity.UserName));
                    }
                    if (fields.Contains(TTagProperties.Name))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TTagProperties.Name, entity.Name));
                    }
                }
                query.UpdateBuilders.Add(builder);
            }
            return session.GetQueryOperator().UpdateAll<TTag>(session, query);
        }
        #endregion
        #region 读
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static TTag DbSelect(this TTag entity, DbSession session, SelectBuilder select)
        {
            var query = session.GetDbQueryBuilder();
            query.SelectBuilder = select;
            return session.GetQueryOperator().Select<TTag>(session, query);
        }
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static TTag DbSelect(this TTag entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentSelect.Add(TTagProperties.UserName);
                builder.ComponentSelect.Add(TTagProperties.TagId);
                builder.ComponentSelect.Add(TTagProperties.Name);
            }
            else
            {
                builder.ComponentSelect.Add(TTagProperties.TagId);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Add(field);
                }
            }
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TTagProperties.TagId, entity.TagId, LocateType.Equal));
            query.SelectBuilders.Add(builder);
            return session.GetQueryOperator().Select<TTag>(session, query);
        }
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static List<TTag> DbSelect(this List<TTag> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentSelect.Add(TTagProperties.UserName);
                builder.ComponentSelect.Add(TTagProperties.TagId);
                builder.ComponentSelect.Add(TTagProperties.Name);
            }
            else
            {
                builder.ComponentSelect.Add(TTagProperties.TagId);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Add(field);
                }
            }
            var Ids = entities.Select(c =>c.TagId );
            if (Ids.Count() != 0)
            {
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TTagProperties.TagId, Ids, LocateType.In));
            }
            query.SelectBuilders.Add(builder);
            return session.GetQueryOperator().SelectAll<TTag>(session, query);
        }
        /// <summary>
        /// 存在相应对象时返回true,缺少对象时返回false
        /// </summary>
        public static bool DbLoad(this TTag entity, DbSession session, params PDMDbProperty[] fields)
        {
            var result = entity.DbSelect(session, fields);
            if (result == null)
            {
                return false;
            }
            if (fields.Count() == 0)
            {
                entity.UserName = result.UserName;
                entity.Name = result.Name;
            }
            else
            {
                if (fields.Contains(TTagProperties.UserName))
                {
                    entity.UserName = result.UserName;
                }
                if (fields.Contains(TTagProperties.Name))
                {
                    entity.Name = result.Name;
                }
            }
            return true;
        }
        /// <summary>
        /// 存在相应对象时返回true,缺少对象时返回false
        /// </summary>
        public static bool DbLoad(this List<TTag> entities, DbSession session, params PDMDbProperty[] fields)
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
