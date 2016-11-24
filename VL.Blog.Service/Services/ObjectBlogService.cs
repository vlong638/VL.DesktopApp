﻿using System;
using System.Collections.Generic;
using VL.Blog.Business;
using VL.Blog.Service.Utilities;
using VL.Common.Core.ORM;
using VL.Common.Core.Protocol;
using VL.Common.Core.Object.VL.Blog;
using VL.User.Service.Utilities;

namespace VL.Blog.Service.Services
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    public class ObjectBlogService : IObjectBlogService
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

        public Report<List<TBlog>> GetAllBlogs()
        {
            return ServiceBase.ServiceContext.ServiceDelegator.HandleEvent(DbConfigOfBlog.DbName, (session) =>
            {
                var data= new List<TBlog>().DbSelect(session);
                if (data == null)
                    return TBlogDomain.ReportHelper.GetReport(data, nameof(GetAllBlogs), CProtocol.CReport.CError);
                return TBlogDomain.ReportHelper.GetReport(data, nameof(GetAllBlogs), CProtocol.CReport.CSuccess);
            });
        }
        public Report<TBlog> GetBlogBody(Guid blogId)
        {
            return ServiceBase.ServiceContext.ServiceDelegator.HandleEvent(DbConfigOfBlog.DbName, (session) =>
            {
                var data = new TBlog(blogId).DbSelect(session);
                if (data == null)
                    return TBlogDomain.ReportHelper.GetReport(data, nameof(GetAllBlogs), CProtocol.CReport.CError);
                return TBlogDomain.ReportHelper.GetReport(data, nameof(GetAllBlogs), CProtocol.CReport.CSuccess);
            });
        }
        public Report<TBlogDetail> GetBlogDetail(Guid blogId)
        {
            return ServiceBase.ServiceContext.ServiceDelegator.HandleEvent(DbConfigOfBlog.DbName, (session) =>
            {
                var data = new TBlogDetail(blogId).DbSelect(session);
                if (data == null)
                    return TBlogDomain.ReportHelper.GetReport(data, nameof(GetAllBlogs), CProtocol.CReport.CError);
                return TBlogDomain.ReportHelper.GetReport(data, nameof(GetAllBlogs), CProtocol.CReport.CSuccess);
            });
        }
        public Report<List<TBlogTagMapper>> GetBlogTags(Guid blogId)
        {
            return ServiceBase.ServiceContext.ServiceDelegator.HandleEvent(DbConfigOfBlog.DbName, (session) =>
            {
                var query = session.GetDbQueryBuilder().SelectBuilder;
                query.ComponentWhere.Add(TBlogTagMapperProperties.BlogId == blogId);
                var data = session.GetQueryOperator().SelectAll<TBlogTagMapper>(query);
                if (data == null)
                    return TBlogDomain.ReportHelper.GetReport(data, nameof(GetBlogTags), CProtocol.CReport.CError);
                return TBlogDomain.ReportHelper.GetReport(data, nameof(GetBlogTags), CProtocol.CReport.CSuccess);
            });
        }
        public Report<List<TBlog>> GetVisibleBlogs()
        {
            return ServiceBase.ServiceContext.ServiceDelegator.HandleEvent(DbConfigOfBlog.DbName, (session) =>
            {
                var query = session.GetDbQueryBuilder().SelectBuilder;
                query.ComponentWhere.Add(TBlogProperties.IsVisible, true, LocateType.Equal);
                var result = session.GetQueryOperator().SelectAll<TBlog>(query);
                if (result==null)
                    return TBlogDomain.ReportHelper.GetReport(result, nameof(GetVisibleBlogs), CProtocol.CReport.CError);
                return TBlogDomain.ReportHelper.GetReport(result, nameof(GetVisibleBlogs), CProtocol.CReport.CSuccess);
            });
        }
    }
}
