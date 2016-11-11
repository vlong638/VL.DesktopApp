using System.Collections.Generic;
using System.ServiceModel;
using VL.Common.Protocol;
using VL.User.Objects.Entities;

namespace VL.User.Service.Services
{
    [ServiceContract]
    public interface IObjectUserService : IWCFServiceNode
    {
        [OperationContract]
        Report<List<TUser>> GetAllUsers();
    }
}
