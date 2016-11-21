using System;
using System.Collections.Generic;
using VL.Blog.Business;
using VL.Blog.Service.Utilities;
using VL.Common.DAS;
using VL.Common.Object.Protocol;
using VL.Common.Object.VL.Blog;
using VL.Common.Protocol;
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
            return ServiceBase.ServiceContext.ServiceDelegator.HandleEvent<List<TBlog>>(DbConfigOfBlog.DbName, (session) =>
            {
                var data= new List<TBlog>().DbSelect(session);
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
    }
}
