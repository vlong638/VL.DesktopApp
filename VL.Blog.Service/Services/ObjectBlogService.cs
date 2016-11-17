using System;
using System.Collections.Generic;
using VL.Blog.Objects.Entities;
using VL.Blog.Service.Utilities;
using VL.Common.Constraints.Protocol;
using VL.Common.DAS;
using VL.Common.Protocol;
using VL.User.Service.Configs;

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

        public List<TBlog> GetAllBlogs()
        {
            using (DbSession session = ServiceBase.ServiceContext.GetDbSession(DbConfigOfBlog.DbName))
            {
                return new List<TBlog>().DbSelect(session);
            }
        }
        public TBlogDetail GetBlogDetail(Guid blogId)
        {
            using (DbSession session = ServiceBase.ServiceContext.GetDbSession(DbConfigOfBlog.DbName))
            {
                return new TBlogDetail(blogId).DbSelect(session);
            }
        }
    }
}
