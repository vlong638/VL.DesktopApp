using System.Collections.Generic;
using VL.Common.Core.Protocol;
using VL.Common.Object.VL.User;
using VL.User.Business;
using VL.User.Service.Utilities;

namespace VL.User.Service.Services
{
    public class ObjectUserService : IObjectUserService
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

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public Report<List<TUser>> GetAllUsers()
        {
            return ServiceBase.ServiceContext.ServiceDelegator.HandleTransactionEvent(DbConfigOfUser.DbName, (session) =>
            {
                Report<List<TUser>> result = new Report<List<TUser>>();
                result.Data = new List<TUser>().DbSelect(session);
                return result;
            });
        }
        /// <summary>
        /// 检测用户角色
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public Report CheckUserInRole(TUser user, List<ERole> roles)
        {
            return ServiceBase.ServiceContext.ServiceDelegator.HandleTransactionEvent(DbConfigOfUser.DbName, (session) =>
            {
                return user.IsInRole(session, roles);
            });
        }
    }
}
