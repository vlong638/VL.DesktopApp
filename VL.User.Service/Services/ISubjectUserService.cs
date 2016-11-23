using System.ServiceModel;
using VL.Common.Core.Protocol;
using VL.Common.Object.VL.User;

namespace VL.User.Service.Services
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface ISubjectUserService :IWCFServiceNode
    {
        [OperationContract]
        Report CreateUser(TUser user);
        [OperationContract]
        Report<ESignInStatus> AuthenticateUser(TUser user, bool rememberMe, bool shouldLockout);
    }
}
