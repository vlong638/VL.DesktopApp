using System;
using System.Collections.Generic;
using System.Linq;
using VL.Common.Core.DAS;
using VL.Common.Core.ORM;
using VL.Common.Core.Protocol;
using VL.Common.Core.Object.VL.Blog;

namespace VL.Blog.Business
{
    public static partial class EntityOperator
    {
        #region Methods
        #region 写
        public static bool DbDelete(this TBlogTagMapper entity, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogTagMapperProperties.BlogId, entity.BlogId, LocateType.Equal));
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogTagMapperProperties.TagName, entity.TagName, LocateType.Equal));
            return session.GetQueryOperator().Delete<TBlogTagMapper>(query);
        }
        public static bool DbDelete(this List<TBlogTagMapper> entities, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            var Ids = entities.Select(c =>c.BlogId );
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogTagMapperProperties.BlogId, Ids, LocateType.In));
            return session.GetQueryOperator().Delete<TBlogTagMapper>(query);
        }
        public static bool DbInsert(this TBlogTagMapper entity, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            InsertBuilder builder = new InsertBuilder();
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogTagMapperProperties.BlogId, entity.BlogId));
            if (entity.TagName == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.TagName));
            }
            if (entity.TagName.Length > 20)
            {
                throw new NotImplementedException(string.Format("参数项:{0}长度:{1}超过额定限制:{2}", nameof(entity.TagName), entity.TagName.Length, 20));
            }
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogTagMapperProperties.TagName, entity.TagName));
            query.InsertBuilders.Add(builder);
            return session.GetQueryOperator().Insert<TBlogTagMapper>(query);
        }
        public static bool DbInsert(this List<TBlogTagMapper> entities, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            foreach (var entity in entities)
            {
                InsertBuilder builder = new InsertBuilder();
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogTagMapperProperties.BlogId, entity.BlogId));
            if (entity.TagName == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.TagName));
            }
            if (entity.TagName.Length > 20)
            {
                throw new NotImplementedException(string.Format("参数项:{0}长度:{1}超过额定限制:{2}", nameof(entity.TagName), entity.TagName.Length, 20));
            }
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogTagMapperProperties.TagName, entity.TagName));
                query.InsertBuilders.Add(builder);
            }
            return session.GetQueryOperator().InsertAll<TBlogTagMapper>(query);
        }
        public static bool DbUpdate(this TBlogTagMapper entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            UpdateBuilder builder = new UpdateBuilder();
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogTagMapperProperties.BlogId, entity.BlogId, LocateType.Equal));
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogTagMapperProperties.TagName, entity.TagName, LocateType.Equal));
            if (fields==null|| fields.Length==0)
            {
            }
            else
            {
            }
            query.UpdateBuilders.Add(builder);
            return session.GetQueryOperator().Update<TBlogTagMapper>(query);
        }
        public static bool DbUpdate(this List<TBlogTagMapper> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            foreach (var entity in entities)
            {
                UpdateBuilder builder = new UpdateBuilder();
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogTagMapperProperties.BlogId, entity.BlogId, LocateType.Equal));
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogTagMapperProperties.TagName, entity.TagName, LocateType.Equal));
                if (fields==null|| fields.Length==0)
                {
                }
                else
                {
                }
                query.UpdateBuilders.Add(builder);
            }
            return session.GetQueryOperator().UpdateAll<TBlogTagMapper>(query);
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
            return session.GetQueryOperator().Select<TBlogTagMapper>(query);
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
                builder.ComponentSelect.Add(TBlogTagMapperProperties.BlogId);
                builder.ComponentSelect.Add(TBlogTagMapperProperties.TagName);
            }
            else
            {
                builder.ComponentSelect.Add(TBlogTagMapperProperties.BlogId);
                builder.ComponentSelect.Add(TBlogTagMapperProperties.TagName);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Add(field);
                }
            }
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogTagMapperProperties.BlogId, entity.BlogId, LocateType.Equal));
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogTagMapperProperties.TagName, entity.TagName, LocateType.Equal));
            query.SelectBuilders.Add(builder);
            return session.GetQueryOperator().Select<TBlogTagMapper>(query);
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
                builder.ComponentSelect.Add(TBlogTagMapperProperties.BlogId);
                builder.ComponentSelect.Add(TBlogTagMapperProperties.TagName);
            }
            else
            {
                builder.ComponentSelect.Add(TBlogTagMapperProperties.BlogId);
                builder.ComponentSelect.Add(TBlogTagMapperProperties.TagName);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Add(field);
                }
            }
            var Ids = entities.Select(c =>c.BlogId );
            if (Ids.Count() != 0)
            {
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogTagMapperProperties.BlogId, Ids, LocateType.In));
            }
            query.SelectBuilders.Add(builder);
            return session.GetQueryOperator().SelectAll<TBlogTagMapper>(query);
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
