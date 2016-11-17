using System;
using System.Collections.Generic;
using VL.Common.Object.ORM;
using VL.Common.ORM;
using System.Linq;
using VL.Common.DAS;
using VL.Common.Protocol;

namespace VL.Blog.Objects.Entities
{
    public static partial class EntityOperator
    {
        #region Methods
        #region 写
        public static bool DbDelete(this TBlogDetail entity, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogDetailProperties.BlogId, entity.BlogId, LocateType.Equal));
            return session.GetQueryOperator().Delete<TBlogDetail>(session, query);
        }
        public static bool DbDelete(this List<TBlogDetail> entities, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            var Ids = entities.Select(c =>c.BlogId );
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogDetailProperties.BlogId, Ids, LocateType.In));
            return session.GetQueryOperator().Delete<TBlogDetail>(session, query);
        }
        public static bool DbInsert(this TBlogDetail entity, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            InsertBuilder builder = new InsertBuilder();
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogDetailProperties.BlogId, entity.BlogId));
            if (entity.Content == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Content));
            }
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogDetailProperties.Content, entity.Content));
            query.InsertBuilders.Add(builder);
            return session.GetQueryOperator().Insert<TBlogDetail>(session, query);
        }
        public static bool DbInsert(this List<TBlogDetail> entities, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            foreach (var entity in entities)
            {
                InsertBuilder builder = new InsertBuilder();
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogDetailProperties.BlogId, entity.BlogId));
            if (entity.Content == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Content));
            }
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogDetailProperties.Content, entity.Content));
                query.InsertBuilders.Add(builder);
            }
            return session.GetQueryOperator().InsertAll<TBlogDetail>(session, query);
        }
        public static bool DbUpdate(this TBlogDetail entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            UpdateBuilder builder = new UpdateBuilder();
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogDetailProperties.BlogId, entity.BlogId, LocateType.Equal));
            if (fields==null|| fields.Length==0)
            {
                builder.ComponentSet.Add(new ComponentValueOfSet(TBlogDetailProperties.BlogId, entity.BlogId));
                builder.ComponentSet.Add(new ComponentValueOfSet(TBlogDetailProperties.Content, entity.Content));
            }
            else
            {
                if (fields.Contains(TBlogDetailProperties.Content))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TBlogDetailProperties.Content, entity.Content));
                }
            }
            query.UpdateBuilders.Add(builder);
            return session.GetQueryOperator().Update<TBlogDetail>(session, query);
        }
        public static bool DbUpdate(this List<TBlogDetail> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            foreach (var entity in entities)
            {
                UpdateBuilder builder = new UpdateBuilder();
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogDetailProperties.BlogId, entity.BlogId, LocateType.Equal));
                if (fields==null|| fields.Length==0)
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TBlogDetailProperties.BlogId, entity.BlogId));
                    builder.ComponentSet.Add(new ComponentValueOfSet(TBlogDetailProperties.Content, entity.Content));
                }
                else
                {
                    if (fields.Contains(TBlogDetailProperties.Content))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TBlogDetailProperties.Content, entity.Content));
                    }
                }
                query.UpdateBuilders.Add(builder);
            }
            return session.GetQueryOperator().UpdateAll<TBlogDetail>(session, query);
        }
        #endregion
        #region 读
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static TBlogDetail DbSelect(this TBlogDetail entity, DbSession session, SelectBuilder select)
        {
            var query = session.GetDbQueryBuilder();
            query.SelectBuilder = select;
            return session.GetQueryOperator().Select<TBlogDetail>(session, query);
        }
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static TBlogDetail DbSelect(this TBlogDetail entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentSelect.Add(TBlogDetailProperties.BlogId);
                builder.ComponentSelect.Add(TBlogDetailProperties.Content);
            }
            else
            {
                builder.ComponentSelect.Add(TBlogDetailProperties.BlogId);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Add(field);
                }
            }
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogDetailProperties.BlogId, entity.BlogId, LocateType.Equal));
            query.SelectBuilders.Add(builder);
            return session.GetQueryOperator().Select<TBlogDetail>(session, query);
        }
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static List<TBlogDetail> DbSelect(this List<TBlogDetail> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentSelect.Add(TBlogDetailProperties.BlogId);
                builder.ComponentSelect.Add(TBlogDetailProperties.Content);
            }
            else
            {
                builder.ComponentSelect.Add(TBlogDetailProperties.BlogId);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Add(field);
                }
            }
            var Ids = entities.Select(c =>c.BlogId );
            if (Ids.Count() != 0)
            {
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogDetailProperties.BlogId, Ids, LocateType.In));
            }
            query.SelectBuilders.Add(builder);
            return session.GetQueryOperator().SelectAll<TBlogDetail>(session, query);
        }
        /// <summary>
        /// 存在相应对象时返回true,缺少对象时返回false
        /// </summary>
        public static bool DbLoad(this TBlogDetail entity, DbSession session, params PDMDbProperty[] fields)
        {
            var result = entity.DbSelect(session, fields);
            if (result == null)
            {
                return false;
            }
            if (fields.Count() == 0)
            {
                entity.Content = result.Content;
            }
            else
            {
                if (fields.Contains(TBlogDetailProperties.Content))
                {
                    entity.Content = result.Content;
                }
            }
            return true;
        }
        /// <summary>
        /// 存在相应对象时返回true,缺少对象时返回false
        /// </summary>
        public static bool DbLoad(this List<TBlogDetail> entities, DbSession session, params PDMDbProperty[] fields)
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
