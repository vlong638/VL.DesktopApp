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
        public static bool DbDelete(this TUserBasicInfo entity, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TUserBasicInfoProperties.UserName, entity.UserName, LocateType.Equal));
            return IORMProvider.GetQueryOperator(session).Delete<TUserBasicInfo>(session, query);
        }
        public static bool DbDelete(this List<TUserBasicInfo> entities, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            var Ids = entities.Select(c =>c.UserName );
            query.DeleteBuilder.ComponentWhere.Add(new ComponentValueOfWhere(TUserBasicInfoProperties.UserName, Ids, LocateType.In));
            return IORMProvider.GetQueryOperator(session).Delete<TUserBasicInfo>(session, query);
        }
        public static bool DbInsert(this TUserBasicInfo entity, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            InsertBuilder builder = new InsertBuilder();
            if (entity.UserName == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.UserName));
            }
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TUserBasicInfoProperties.UserName, entity.UserName));
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TUserBasicInfoProperties.Gender, entity.Gender));
            if (entity.Birthday.HasValue)
            {
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TUserBasicInfoProperties.Birthday, entity.Birthday.Value));
            }
            if (entity.Mobile.HasValue)
            {
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TUserBasicInfoProperties.Mobile, entity.Mobile.Value));
            }
            if (entity.Email == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Email));
            }
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TUserBasicInfoProperties.Email, entity.Email));
            if (entity.IdCardNumber == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.IdCardNumber));
            }
            builder.ComponentInsert.Add(new ComponentValueOfInsert(TUserBasicInfoProperties.IdCardNumber, entity.IdCardNumber));
            query.InsertBuilders.Add(builder);
            return IORMProvider.GetQueryOperator(session).Insert<TUserBasicInfo>(session, query);
        }
        public static bool DbInsert(this List<TUserBasicInfo> entities, DbSession session)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            foreach (var entity in entities)
            {
                InsertBuilder builder = new InsertBuilder();
            if (entity.UserName == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.UserName));
            }
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TUserBasicInfoProperties.UserName, entity.UserName));
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TUserBasicInfoProperties.Gender, entity.Gender));
                if (entity.Birthday.HasValue)
                {
                    builder.ComponentInsert.Add(new ComponentValueOfInsert(TUserBasicInfoProperties.Birthday, entity.Birthday.Value));
                }
                if (entity.Mobile.HasValue)
                {
                    builder.ComponentInsert.Add(new ComponentValueOfInsert(TUserBasicInfoProperties.Mobile, entity.Mobile.Value));
                }
            if (entity.Email == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.Email));
            }
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TUserBasicInfoProperties.Email, entity.Email));
            if (entity.IdCardNumber == null)
            {
                throw new NotImplementedException("缺少必填的参数项值, 参数项: " + nameof(entity.IdCardNumber));
            }
                builder.ComponentInsert.Add(new ComponentValueOfInsert(TUserBasicInfoProperties.IdCardNumber, entity.IdCardNumber));
                query.InsertBuilders.Add(builder);
            }
            return IORMProvider.GetQueryOperator(session).InsertAll<TUserBasicInfo>(session, query);
        }
        public static bool DbUpdate(this TUserBasicInfo entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            UpdateBuilder builder = new UpdateBuilder();
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TUserBasicInfoProperties.UserName, entity.UserName, LocateType.Equal));
            if (fields==null|| fields.Length==0)
            {
                builder.ComponentSet.Add(new ComponentValueOfSet(TUserBasicInfoProperties.UserName, entity.UserName));
                builder.ComponentSet.Add(new ComponentValueOfSet(TUserBasicInfoProperties.Gender, entity.Gender));
                builder.ComponentSet.Add(new ComponentValueOfSet(TUserBasicInfoProperties.Birthday, entity.Birthday));
                builder.ComponentSet.Add(new ComponentValueOfSet(TUserBasicInfoProperties.Mobile, entity.Mobile));
                builder.ComponentSet.Add(new ComponentValueOfSet(TUserBasicInfoProperties.Email, entity.Email));
                builder.ComponentSet.Add(new ComponentValueOfSet(TUserBasicInfoProperties.IdCardNumber, entity.IdCardNumber));
            }
            else
            {
                if (fields.Contains(TUserBasicInfoProperties.Gender))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TUserBasicInfoProperties.Gender, entity.Gender));
                }
                if (fields.Contains(TUserBasicInfoProperties.Birthday))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TUserBasicInfoProperties.Birthday, entity.Birthday));
                }
                if (fields.Contains(TUserBasicInfoProperties.Mobile))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TUserBasicInfoProperties.Mobile, entity.Mobile));
                }
                if (fields.Contains(TUserBasicInfoProperties.Email))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TUserBasicInfoProperties.Email, entity.Email));
                }
                if (fields.Contains(TUserBasicInfoProperties.IdCardNumber))
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TUserBasicInfoProperties.IdCardNumber, entity.IdCardNumber));
                }
            }
            query.UpdateBuilders.Add(builder);
            return IORMProvider.GetQueryOperator(session).Update<TUserBasicInfo>(session, query);
        }
        public static bool DbUpdate(this List<TUserBasicInfo> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            foreach (var entity in entities)
            {
                UpdateBuilder builder = new UpdateBuilder();
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TUserBasicInfoProperties.UserName, entity.UserName, LocateType.Equal));
                if (fields==null|| fields.Length==0)
                {
                    builder.ComponentSet.Add(new ComponentValueOfSet(TUserBasicInfoProperties.UserName, entity.UserName));
                    builder.ComponentSet.Add(new ComponentValueOfSet(TUserBasicInfoProperties.Gender, entity.Gender));
                    builder.ComponentSet.Add(new ComponentValueOfSet(TUserBasicInfoProperties.Birthday, entity.Birthday));
                    builder.ComponentSet.Add(new ComponentValueOfSet(TUserBasicInfoProperties.Mobile, entity.Mobile));
                    builder.ComponentSet.Add(new ComponentValueOfSet(TUserBasicInfoProperties.Email, entity.Email));
                    builder.ComponentSet.Add(new ComponentValueOfSet(TUserBasicInfoProperties.IdCardNumber, entity.IdCardNumber));
                }
                else
                {
                    if (fields.Contains(TUserBasicInfoProperties.Gender))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TUserBasicInfoProperties.Gender, entity.Gender));
                    }
                    if (fields.Contains(TUserBasicInfoProperties.Birthday))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TUserBasicInfoProperties.Birthday, entity.Birthday));
                    }
                    if (fields.Contains(TUserBasicInfoProperties.Mobile))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TUserBasicInfoProperties.Mobile, entity.Mobile));
                    }
                    if (fields.Contains(TUserBasicInfoProperties.Email))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TUserBasicInfoProperties.Email, entity.Email));
                    }
                    if (fields.Contains(TUserBasicInfoProperties.IdCardNumber))
                    {
                        builder.ComponentSet.Add(new ComponentValueOfSet(TUserBasicInfoProperties.IdCardNumber, entity.IdCardNumber));
                    }
                }
                query.UpdateBuilders.Add(builder);
            }
            return IORMProvider.GetQueryOperator(session).UpdateAll<TUserBasicInfo>(session, query);
        }
        #endregion
        #region 读
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static TUserBasicInfo DbSelect(this TUserBasicInfo entity, DbSession session, SelectBuilder select)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            query.SelectBuilder = select;
            return IORMProvider.GetQueryOperator(session).Select<TUserBasicInfo>(session, query);
        }
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static TUserBasicInfo DbSelect(this TUserBasicInfo entity, DbSession session, params PDMDbProperty[] fields)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentSelect.Add(TUserBasicInfoProperties.UserName);
                builder.ComponentSelect.Add(TUserBasicInfoProperties.Gender);
                builder.ComponentSelect.Add(TUserBasicInfoProperties.Birthday);
                builder.ComponentSelect.Add(TUserBasicInfoProperties.Mobile);
                builder.ComponentSelect.Add(TUserBasicInfoProperties.Email);
                builder.ComponentSelect.Add(TUserBasicInfoProperties.IdCardNumber);
            }
            else
            {
                builder.ComponentSelect.Add(TUserBasicInfoProperties.UserName);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Add(field);
                }
            }
            builder.ComponentWhere.Add(new ComponentValueOfWhere(TUserBasicInfoProperties.UserName, entity.UserName, LocateType.Equal));
            query.SelectBuilders.Add(builder);
            return IORMProvider.GetQueryOperator(session).Select<TUserBasicInfo>(session, query);
        }
        /// <summary>
        /// 未查询到数据时返回 null
        /// </summary>
        public static List<TUserBasicInfo> DbSelect(this List<TUserBasicInfo> entities, DbSession session, params PDMDbProperty[] fields)
        {
            var query = IORMProvider.GetDbQueryBuilder(session);
            SelectBuilder builder = new SelectBuilder();
            if (fields.Count() == 0)
            {
                builder.ComponentSelect.Add(TUserBasicInfoProperties.UserName);
                builder.ComponentSelect.Add(TUserBasicInfoProperties.Gender);
                builder.ComponentSelect.Add(TUserBasicInfoProperties.Birthday);
                builder.ComponentSelect.Add(TUserBasicInfoProperties.Mobile);
                builder.ComponentSelect.Add(TUserBasicInfoProperties.Email);
                builder.ComponentSelect.Add(TUserBasicInfoProperties.IdCardNumber);
            }
            else
            {
                builder.ComponentSelect.Add(TUserBasicInfoProperties.UserName);
                foreach (var field in fields)
                {
                    builder.ComponentSelect.Add(field);
                }
            }
            var Ids = entities.Select(c =>c.UserName );
            if (Ids.Count() != 0)
            {
                builder.ComponentWhere.Add(new ComponentValueOfWhere(TUserBasicInfoProperties.UserName, Ids, LocateType.In));
            }
            query.SelectBuilders.Add(builder);
            return IORMProvider.GetQueryOperator(session).SelectAll<TUserBasicInfo>(session, query);
        }
        /// <summary>
        /// 存在相应对象时返回true,缺少对象时返回false
        /// </summary>
        public static bool DbLoad(this TUserBasicInfo entity, DbSession session, params PDMDbProperty[] fields)
        {
            var result = entity.DbSelect(session, fields);
            if (result == null)
            {
                return false;
            }
            if (fields.Count() == 0)
            {
                entity.Gender = result.Gender;
                entity.Birthday = result.Birthday;
                entity.Mobile = result.Mobile;
                entity.Email = result.Email;
                entity.IdCardNumber = result.IdCardNumber;
            }
            else
            {
                if (fields.Contains(TUserBasicInfoProperties.Gender))
                {
                    entity.Gender = result.Gender;
                }
                if (fields.Contains(TUserBasicInfoProperties.Birthday))
                {
                    entity.Birthday = result.Birthday;
                }
                if (fields.Contains(TUserBasicInfoProperties.Mobile))
                {
                    entity.Mobile = result.Mobile;
                }
                if (fields.Contains(TUserBasicInfoProperties.Email))
                {
                    entity.Email = result.Email;
                }
                if (fields.Contains(TUserBasicInfoProperties.IdCardNumber))
                {
                    entity.IdCardNumber = result.IdCardNumber;
                }
            }
            return true;
        }
        /// <summary>
        /// 存在相应对象时返回true,缺少对象时返回false
        /// </summary>
        public static bool DbLoad(this List<TUserBasicInfo> entities, DbSession session, params PDMDbProperty[] fields)
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
