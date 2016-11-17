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
        public static bool DbDelete(this TBlog entity, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogProperties.BlogId, entity.BlogId, LocateType.Equal));
            return session.GetQueryOperator().Delete<TBlog>(session, query);
        }
        public static bool DbDelete(this List<TBlog> entities, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            var Ids = entities.Select(c =>c.BlogId );
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogProperties.BlogId, Ids, LocateType.In));
            return session.GetQueryOperator().Delete<TBlog>(session, query);
        }
        public static bool DbInsert(this TBlog entity, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            InsertBuilder builder = new InsertBuilder();
            if (entity.UserName == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.UserName));
            }
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogProperties.UserName, entity.UserName));
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogProperties.BlogId, entity.BlogId));
            if (entity.Title == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Title));
            }
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogProperties.Title, entity.Title));
            if (entity.BreviaryContent == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.BreviaryContent));
            }
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogProperties.BreviaryContent, entity.BreviaryContent));
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogProperties.CreatedTime, entity.CreatedTime));
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogProperties.LastEditTime, entity.LastEditTime));
            query.InsertBuilders.Add(builder);
            return session.GetQueryOperator().Insert<TBlog>(session, query);
        }
        public static bool DbInsert(this List<TBlog> entities, DbSession session)
        {
            var query = session.GetDbQueryBuilder();
            foreach (var entity in entities)
            {
                InsertBuilder builder = new InsertBuilder();
            if (entity.UserName == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.UserName));
            }
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogProperties.UserName, entity.UserName));
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogProperties.BlogId, entity.BlogId));
            if (entity.Title == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Title));
            }
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogProperties.Title, entity.Title));
            if (entity.BreviaryContent == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.BreviaryContent));
            }
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogProperties.BreviaryContent, entity.BreviaryContent));
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogProperties.CreatedTime, entity.CreatedTime));
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TBlogProperties.LastEditTime, entity.LastEditTime));
                query.InsertBuilders.Add(builder);
            }
            return session.GetQueryOperator().InsertAll<TBlog>(session, query);
        }
        public static bool DbUpdate(this TBlog entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            UpdateBuilder builder = new UpdateBuilder();
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogProperties.BlogId, entity.BlogId, LocateType.Equal));
            if (fields==null|| fields.Length==0)
            {
                builder.ComponentSet.Add(new ComponentValueOfSet(TBlogProperties.UserName, entity.UserName));
                builder.ComponentSet.Add(new ComponentValueOfSet(TBlogProperties.BlogId, entity.BlogId));
                builder.ComponentSet.Add(new ComponentValueOfSet(TBlogProperties.Title, entity.Title));
                builder.ComponentSet.Add(new ComponentValueOfSet(TBlogProperties.BreviaryContent, entity.BreviaryContent));
                builder.ComponentSet.Add(new ComponentValueOfSet(TBlogProperties.CreatedTime, entity.CreatedTime));
                builder.ComponentSet.Add(new ComponentValueOfSet(TBlogProperties.LastEditTime, entity.LastEditTime));
            }
            else
            {
                if (fields.Contains(TBlogProperties.UserName))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TBlogProperties.UserName, entity.UserName));
                }
                if (fields.Contains(TBlogProperties.Title))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TBlogProperties.Title, entity.Title));
                }
                if (fields.Contains(TBlogProperties.BreviaryContent))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TBlogProperties.BreviaryContent, entity.BreviaryContent));
                }
                if (fields.Contains(TBlogProperties.CreatedTime))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TBlogProperties.CreatedTime, entity.CreatedTime));
                }
                if (fields.Contains(TBlogProperties.LastEditTime))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TBlogProperties.LastEditTime, entity.LastEditTime));
                }
            }
            query.UpdateBuilders.Add(builder);
            return session.GetQueryOperator().Update<TBlog>(session, query);
        }
        public static bool DbUpdate(this List<TBlog> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            foreach (var entity in entities)
            {
                UpdateBuilder builder = new UpdateBuilder();
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogProperties.BlogId, entity.BlogId, LocateType.Equal));
                if (fields==null|| fields.Length==0)
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TBlogProperties.UserName, entity.UserName));
                    builder.ComponentSet.Add(new ComponentValueOfSet(TBlogProperties.BlogId, entity.BlogId));
                    builder.ComponentSet.Add(new ComponentValueOfSet(TBlogProperties.Title, entity.Title));
                    builder.ComponentSet.Add(new ComponentValueOfSet(TBlogProperties.BreviaryContent, entity.BreviaryContent));
                    builder.ComponentSet.Add(new ComponentValueOfSet(TBlogProperties.CreatedTime, entity.CreatedTime));
                    builder.ComponentSet.Add(new ComponentValueOfSet(TBlogProperties.LastEditTime, entity.LastEditTime));
                }
                else
                {
                    if (fields.Contains(TBlogProperties.UserName))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TBlogProperties.UserName, entity.UserName));
                    }
                    if (fields.Contains(TBlogProperties.Title))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TBlogProperties.Title, entity.Title));
                    }
                    if (fields.Contains(TBlogProperties.BreviaryContent))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TBlogProperties.BreviaryContent, entity.BreviaryContent));
                    }
                    if (fields.Contains(TBlogProperties.CreatedTime))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TBlogProperties.CreatedTime, entity.CreatedTime));
                    }
                    if (fields.Contains(TBlogProperties.LastEditTime))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TBlogProperties.LastEditTime, entity.LastEditTime));
                    }
                }
                query.UpdateBuilders.Add(builder);
            }
            return session.GetQueryOperator().UpdateAll<TBlog>(session, query);
        }
        #endregion
        #region 读
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static TBlog DbSelect(this TBlog entity, DbSession session, SelectBuilder select)
        {
            var query = session.GetDbQueryBuilder();
            query.SelectBuilder = select;
            return session.GetQueryOperator().Select<TBlog>(session, query);
        }
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static TBlog DbSelect(this TBlog entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentSelect.Add(TBlogProperties.UserName);
                builder.ComponentSelect.Add(TBlogProperties.BlogId);
                builder.ComponentSelect.Add(TBlogProperties.Title);
                builder.ComponentSelect.Add(TBlogProperties.BreviaryContent);
                builder.ComponentSelect.Add(TBlogProperties.CreatedTime);
                builder.ComponentSelect.Add(TBlogProperties.LastEditTime);
            }
            else
            {
                builder.ComponentSelect.Add(TBlogProperties.BlogId);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Add(field);
                }
            }
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogProperties.BlogId, entity.BlogId, LocateType.Equal));
            query.SelectBuilders.Add(builder);
            return session.GetQueryOperator().Select<TBlog>(session, query);
        }
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static List<TBlog> DbSelect(this List<TBlog> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = session.GetDbQueryBuilder();
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentSelect.Add(TBlogProperties.UserName);
                builder.ComponentSelect.Add(TBlogProperties.BlogId);
                builder.ComponentSelect.Add(TBlogProperties.Title);
                builder.ComponentSelect.Add(TBlogProperties.BreviaryContent);
                builder.ComponentSelect.Add(TBlogProperties.CreatedTime);
                builder.ComponentSelect.Add(TBlogProperties.LastEditTime);
            }
            else
            {
                builder.ComponentSelect.Add(TBlogProperties.BlogId);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Add(field);
                }
            }
            var Ids = entities.Select(c =>c.BlogId );
            if (Ids.Count() != 0)
            {
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TBlogProperties.BlogId, Ids, LocateType.In));
            }
            query.SelectBuilders.Add(builder);
            return session.GetQueryOperator().SelectAll<TBlog>(session, query);
        }
        /// <summary>
        /// 存在相应对象时返回true,缺少对象时返回false
        /// </summary>
        public static bool DbLoad(this TBlog entity, DbSession session, params PDMDbProperty[] fields)
        {
            var result = entity.DbSelect(session, fields);
            if (result == null)
            {
                return false;
            }
            if (fields.Count() == 0)
            {
                entity.UserName = result.UserName;
                entity.Title = result.Title;
                entity.BreviaryContent = result.BreviaryContent;
                entity.CreatedTime = result.CreatedTime;
                entity.LastEditTime = result.LastEditTime;
            }
            else
            {
                if (fields.Contains(TBlogProperties.UserName))
                {
                    entity.UserName = result.UserName;
                }
                if (fields.Contains(TBlogProperties.Title))
                {
                    entity.Title = result.Title;
                }
                if (fields.Contains(TBlogProperties.BreviaryContent))
                {
                    entity.BreviaryContent = result.BreviaryContent;
                }
                if (fields.Contains(TBlogProperties.CreatedTime))
                {
                    entity.CreatedTime = result.CreatedTime;
                }
                if (fields.Contains(TBlogProperties.LastEditTime))
                {
                    entity.LastEditTime = result.LastEditTime;
                }
            }
            return true;
        }
        /// <summary>
        /// 存在相应对象时返回true,缺少对象时返回false
        /// </summary>
        public static bool DbLoad(this List<TBlog> entities, DbSession session, params PDMDbProperty[] fields)
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
