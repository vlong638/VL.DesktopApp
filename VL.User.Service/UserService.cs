using System;
using System.Collections.Generic;
using VL.Common.Logger.Utilities;
using VL.Common.Protocol;
using VL.Common.Protocol.IService;
using VL.User.Objects.Entities;
using VL.User.Objects.SubResults;
using VL.User.Service.Configs;
using VL.User.Service.DomainEntities;
using VL.User.Service.Utilities;

namespace VL.User.Service
{
    /// <summary>
    /// Service是工作单元块,这里负责单元化的工作处理
    /// 以及服务日志的处理(这里将UnitOfWork无关的部分移至ServiceDelegator,仅留下了纯粹的UnitOfWork)
    /// </summary>
    public class UserService : ISubjectUserService 
    {
        static ServiceContextOfUser ServiceContext { set; get; }
        static DependencyResult DependencyResult { set; get; }

        public bool CheckAlive()
        {
            var result = CheckNodeReferences();
            return result.IsAllDependenciesAvailable;
        }
        public DependencyResult CheckNodeReferences()
        {
            try
            {
                if (DependencyResult == null)
                {
                    ServiceContext = new ServiceContextOfUser();
                }
                DependencyResult = ServiceContext.Init();
            }
            catch (Exception ex)
            {
                LoggerProvider.GetLog4netLogger("ServiceLog").Error(ex.ToString());
            }
            return DependencyResult;
        }

        public Result<CreateUserResult> Register(TUser user)
        {
            ///Service级的方法决定事务操作的框架,以及落地的数据库
            ///Domain级的方法决定领域操作的具体执行逻辑
            ///Entity级的方法
            return ServiceContext.ServiceDelegator.HandleSimpleTransactionEvent(nameof(User), (session) =>
            {
                //return new SubjectOperator().CreateUser(session, user);

                Result<CreateUserResult> result = new Result<CreateUserResult>();
                result.Data = user.Create(session);
                if (result.Data == CreateUserResult.Success)
                {
                    result.ResultCode = EResultCode.Success;
                }
                else
                {
                    result.ResultCode = EResultCode.Failure;
                    switch (result.Data)
                    {
                        case CreateUserResult.DbOperationFailed:
                            result.Message = "操作数据库失败";
                            break;
                        case CreateUserResult.UserNameExist:
                            result.Message = "用户名已存在";
                            break;
                        case CreateUserResult.MobileExist:
                            result.Message = "手机已存在";
                            break;
                        case CreateUserResult.EmailExist:
                            result.Message = "邮箱已存在";
                            break;
                        case CreateUserResult.IdExist:
                            result.Message = "Id已存在";
                            break;
                        default:
                            result.Message = "未支持该错误码:" + result.Data.ToString();
                            break;
                    }
                }
                return result;
            });
        }
        public Result<AuthenticateResult> AuthenticateUser(TUser user)
        {
            return ServiceContext.ServiceDelegator.HandleSimpleTransactionEvent(nameof(User), (session) =>
            {
                //return new SubjectOperator().AuthenticateUser(session, user);

                Result<AuthenticateResult> result = new Result<AuthenticateResult>();
                result.Data = user.Authenticate(session);
                if (result.Data == AuthenticateResult.Success)
                {
                    result.ResultCode = EResultCode.Success;
                }
                else
                {
                    result.ResultCode = EResultCode.Failure;
                    switch (result.Data)
                    {
                        case AuthenticateResult.UserNameUnexist:
                            result.Message = "用户名不存在";
                            break;
                        case AuthenticateResult.PasswordError:
                            result.Message = "密码错误";
                            break;
                        default:
                            result.Message = "未支持该错误码:" + result.Data.ToString();
                            break;
                    }
                }
                return result;
            });
        }
        public Result<List<TUser>> GetAllUsers()
        {
            return ServiceContext.ServiceDelegator.HandleSimpleTransactionEvent(nameof(User), (session) =>
            {
                //return new ObjectOperator().GetAllUsers(session);

                Result<List<TUser>> result = new Result<List<TUser>>();
                result.Data = new List<TUser>().DbSelect(session);
                result.ResultCode = EResultCode.Success;
                return result;
            });
        }

        #region Test
        public int Test()
        {
            LoggerProvider.GetLog4netLogger("ServiceLog").Error("message for test");
            return 1;
        }
        public bool GetIsSQLLogAvailable()
        {
            return ServiceContext.ProtocolConfig.IsSQLLogAvailable.Value;
        }
        public A GetA()
        {
            return new A()
            {
                Name = "A",
                As = new List<A>()
                {
                    new A()
                    {
                        Name ="Sub A"
                    }
                },
                Bs = new List<B>()
                {
                    new B()
                    {
                        Name="Sub B",
                        Value=1
                    }
                },
            };
        }
        #endregion
    }
}
