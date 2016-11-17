using System.Collections.Generic;
using System.ServiceModel;
using VL.Common.Object.Protocol;
using VL.Common.Object.VL.User;
using VL.Common.Protocol;

namespace VL.User.Service.Services
{
    [ServiceContract]
    public interface IObjectUserService : IWCFServiceNode
    {
        [OperationContract]
        Report<List<TUser>> GetAllUsers();
        [OperationContract]
        Report CheckUserInRole(TUser user, List<ERole> roles);
    }
}
