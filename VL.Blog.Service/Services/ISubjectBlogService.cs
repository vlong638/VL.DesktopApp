using System;
using System.Collections.Generic;
using System.ServiceModel;
using VL.Common.Core.Protocol;
using VL.Common.Object.VL.Blog;

namespace VL.Blog.Service.Services
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface ISubjectBlogService: IWCFServiceNode
    {
        [OperationContract]
        Report EditBlog(TBlog blog,string content,List<string> tags);
        [OperationContract]
        Report ChangeVisibility(Guid blogId,bool isVisible);
    }
}
