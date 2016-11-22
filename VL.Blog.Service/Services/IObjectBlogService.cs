using System;
using System.Collections.Generic;
using System.ServiceModel;
using VL.Common.Object.Protocol;
using VL.Common.Object.VL.Blog;
using VL.Common.Protocol;

namespace VL.Blog.Service.Services
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IObjectBlogService: IWCFServiceNode
    {
        [OperationContract]
        Report<List<TBlog>> GetAllBlogs();
        [OperationContract]
        Report<TBlog> GetBlogBody(Guid blogId);
        [OperationContract]
        Report<TBlogDetail> GetBlogDetail(Guid blogId);
        [OperationContract]
        Report<List<TBlog>> GetVisibleBlogs();
    }
}
