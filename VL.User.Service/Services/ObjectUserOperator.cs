using System.Collections.Generic;
using VL.Common.Protocol;
using VL.User.Objects.Entities;
using VL.User.Service.Configs;
using VL.User.Service.Utilities;

namespace VL.User.Service.Services
{
    public class ObjectUserOperator : IObjectUserService
    {
        #region 服务基础
        static BaseWCFServiceNode<ServiceContextOfUser> ServiceBase = new BaseWCFServiceNode<ServiceContextOfUser>();
        public bool CheckAlive()
        {
            return ServiceBase.CheckAlive();
        }
        public DependencyResult CheckNodeReferences()
        {
            return ServiceBase.CheckNodeReferences();
        }
        #endregion

        public Report<List<TUser>> GetAllUsers()
        {
            return ServiceBase.ServiceContext.ServiceDelegator.HandleTransactionEvent(DbConfigOfUser.DbName, (session) =>
            {
                Report<List<TUser>> result = new Report<List<TUser>>();
                result.Data = new List<TUser>().DbSelect(session);
                return result;
            });
        }
    }
}
