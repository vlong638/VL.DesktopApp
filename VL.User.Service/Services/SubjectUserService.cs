using VL.Common.Protocol;
using VL.User.Objects.Entities;
using VL.User.Service.Configs;
using VL.User.Service.Utilities;

namespace VL.User.Service.Services
{
    public class SubjectUserService : ISubjectUserService
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
        /// 创建用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Report CreateUser(TUser user)
        {
            return ServiceBase.ServiceContext.ServiceDelegator.HandleTransactionEvent(DbConfigOfUser.DbName, (session) =>
             {
                 return user.Create(session);
             });
        }
        public Report AuthenticateUser(TUser user, bool rememberMe, bool shouldLockout = false)
        {
            return ServiceBase.ServiceContext.ServiceDelegator.HandleTransactionEvent(DbConfigOfUser.DbName, (session) =>
            {
                return user.Authenticate(session);
            });
        }
    }
}
