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
        public static bool DbDelete(this TBlogTagMapper entity, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogTagMapperProperties.TagId, entity.TagId, LocateType.Equal));
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogTagMapperProperties.BlogId, entity.BlogId, LocateType.Equal));
            return session.GetQueryOperator().Delete<TBlogTagMapper>(session, query);
        }
        public static bool DbDelete(this List<TBlogTagMapper> entities, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            var Ids = entities.Select(c =>c.TagId );
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogTagMapperProperties.TagId, Ids, LocateType.In));
            return session.GetQueryOperator().Delete<TBlogTagMapper>(session, query);
        }
        public static bool DbInsert(this TBlogTagMapper entity, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            InsertBuilder builder = new InsertBuilder();
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogTagMapperProperties.TagId, entity.TagId));
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogTagMapperProperties.BlogId, entity.BlogId));
            query.InsertBuilders.Add(builder);
            return session.GetQueryOperator().Insert<TBlogTagMapper>(session, query);
        }
        public static bool DbInsert(this List<TBlogTagMapper> entities, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            foreach (var entity in entities)
            {
                InsertBuilder builder = new InsertBuilder();
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogTagMapperProperties.TagId, entity.TagId));
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogTagMapperProperties.BlogId, entity.BlogId));
                query.InsertBuilders.Add(builder);
            }
            return session.GetQueryOperator().InsertAll<TBlogTagMapper>(session, query);
        }
        public static bool DbUpdate(this TBlogTagMapper entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            UpdateBuilder builder = new UpdateBuilder();
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogTagMapperProperties.TagId, entity.TagId, LocateType.Equal));
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogTagMapperProperties.BlogId, entity.BlogId, LocateType.Equal));
            if (fields==null|| fields.Length==0)
            {
                builder.ComponentSet.Add(new ComponentValueOfSet(TBlogTagMapperProperties.TagId, entity.TagId));
                builder.ComponentSet.Add(new ComponentValueOfSet(TBlogTagMapperProperties.BlogId, entity.BlogId));
            }
            else
            {
            }
            query.UpdateBuilders.Add(builder);
            return session.GetQueryOperator().Update<TBlogTagMapper>(session, query);
        }
        public static bool DbUpdate(this List<TBlogTagMapper> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            foreach (var entity in entities)
            {
                UpdateBuilder builder = new UpdateBuilder();
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogTagMapperProperties.TagId, entity.TagId, LocateType.Equal));
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogTagMapperProperties.BlogId, entity.BlogId, LocateType.Equal));
                if (fields==null|| fields.Length==0)
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TBlogTagMapperProperties.TagId, entity.TagId));
                    builder.ComponentSet.Add(new ComponentValueOfSet(TBlogTagMapperProperties.BlogId, entity.BlogId));
                }
                else
                {
                }
                query.UpdateBuilders.Add(builder);
            }
            return session.GetQueryOperator().UpdateAll<TBlogTagMapper>(session, query);
        }
        #endregion
        #region 读
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static TBlogTagMapper DbSelect(this TBlogTagMapper entity, DbSession session, SelectBuilder select)
        {
            var query = session.GetDbQueryBuilder();
            query.SelectBuilder = select;
            return session.GetQueryOperator().Select<TBlogTagMapper>(session, query);
        }
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static TBlogTagMapper DbSelect(this TBlogTagMapper entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentSelect.Add(TBlogTagMapperProperties.TagId);
                builder.ComponentSelect.Add(TBlogTagMapperProperties.BlogId);
            }
            else
            {
                builder.ComponentSelect.Add(TBlogTagMapperProperties.TagId);
                builder.ComponentSelect.Add(TBlogTagMapperProperties.BlogId);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Add(field);
                }
            }
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogTagMapperProperties.TagId, entity.TagId, LocateType.Equal));
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogTagMapperProperties.BlogId, entity.BlogId, LocateType.Equal));
            query.SelectBuilders.Add(builder);
            return session.GetQueryOperator().Select<TBlogTagMapper>(session, query);
        }
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static List<TBlogTagMapper> DbSelect(this List<TBlogTagMapper> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentSelect.Add(TBlogTagMapperProperties.TagId);
                builder.ComponentSelect.Add(TBlogTagMapperProperties.BlogId);
            }
            else
            {
                builder.ComponentSelect.Add(TBlogTagMapperProperties.TagId);
                builder.ComponentSelect.Add(TBlogTagMapperProperties.BlogId);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Add(field);
                }
            }
            var Ids = entities.Select(c =>c.TagId );
            if (Ids.Count() != 0)
            {
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogTagMapperProperties.TagId, Ids, LocateType.In));
            }
            query.SelectBuilders.Add(builder);
            return session.GetQueryOperator().SelectAll<TBlogTagMapper>(session, query);
        }
        /// <summary>
        /// 存在相应对象时返回true,缺少对象时返回false
        /// </summary>
        public static bool DbLoad(this TBlogTagMapper entity, DbSession session, params PDMDbProperty[] fields)
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
        public static bool DbLoad(this List<TBlogTagMapper> entities, DbSession session, params PDMDbProperty[] fields)
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
