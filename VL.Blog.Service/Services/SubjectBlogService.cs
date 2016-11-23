﻿using System;
using System.Collections.Generic;
using VL.Blog.Business;
using VL.Blog.Service.Utilities;
using VL.Common.Core.Protocol;
using VL.Common.Object.VL.Blog;
using VL.User.Service.Utilities;

namespace VL.Blog.Service.Services
{
    /// <summary>
    /// 服务层仅关注将业务逻辑包装成服务形式,并为其提供必要的事务支持
    /// </summary>
    public class SubjectBlogService : ISubjectBlogService
    {
        #region 服务基础
        static BaseWCFServiceNode<ServiceContextOfBlog> ServiceBase = new BaseWCFServiceNode<ServiceContextOfBlog>();
        public bool CheckAlive()
        {
            return ServiceBase.CheckAlive();
        }
        public DependencyResult CheckNodeReferences()
        {
            return ServiceBase.CheckNodeReferences();
        }

        #endregion

        public Report EditBlog(TBlog blog, string content, List<string> tags)
        {
            return ServiceBase.ServiceContext.ServiceDelegator.HandleTransactionEvent(DbConfigOfBlog.DbName, (session) =>
            {
                return blog.Edit(session, content, tags);
            });
        }
        public Report ChangeVisibility(Guid blogId, bool isVisible)
        {
            return ServiceBase.ServiceContext.ServiceDelegator.HandleTransactionEvent(DbConfigOfBlog.DbName, (session) =>
            {
                return new TBlog(blogId) { IsVisible = isVisible }.ChangeVisibility(session);
            });
        }
    }
}
