using VL.Blog.Business;
using VL.Blog.Service.Utilities;
using VL.Common.Object.Protocol;
using VL.Common.Object.VL.Blog;
using VL.Common.Protocol;
using VL.User.Service.Utilities;

namespace VL.Blog.Service.Services
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
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

        public Report CreateBlog(TBlog blog,string content)
        {
            return ServiceBase.ServiceContext.ServiceDelegator.HandleTransactionEvent(DbConfigOfBlog.DbName, (session) =>
            {
                return blog.Create(session,content);
            });
        }
    }
}
