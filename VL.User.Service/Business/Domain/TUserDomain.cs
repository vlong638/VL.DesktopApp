using System;
using System.Collections.Generic;
using System.Linq;
using VL.Common.Core.DAS;
using VL.Common.Core.ORM;
using VL.Common.Core.Protocol;
using VL.Common.Object.VL.User;
using VL.User.Service.Utilities;

namespace VL.User.Business
{
    public static class TUserDomain
    {
        static ClassReportHelper ReportHelper { set; get; } = ServiceContextOfUser.ReportHelper.GetClassReportHelper(nameof(TUser));

        #region Create
        enum ECode_Create
        {
            Default = CProtocol.CReport.CManualStart,
            用户名不可为空,
            用户已存在,
            密码不可为空,
            操作数据库失败,
        }
        public static Report Create(this TUser user,DbSession session)
        {
            if (string.IsNullOrEmpty(user.UserName))
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.用户名不可为空);
            if (user.CheckExistance(session))
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.用户已存在);
            if (string.IsNullOrEmpty(user.Password))
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.密码不可为空);

            user.CreateTime = DateTime.Now;
            if (user.DbInsert(session))
                return new Report(CProtocol.CReport.CSuccess);
            else
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.操作数据库失败);
        }
        #endregion

        #region Regist
        enum ECode_Authenticate
        {
            Default = CProtocol.CReport.CManualStart,
            用户名不可为空,
            用户不存在,
            密码不可为空,
            操作数据库失败,
        }
        public static Report<ESignInStatus> Authenticate(this TUser user, DbSession session)
        {
            if (string.IsNullOrEmpty(user.UserName))
                return ReportHelper.GetReport(ESignInStatus.Failure, nameof(Create), (int)ECode_Authenticate.用户名不可为空);
            if (!user.CheckExistance(session))
                return ReportHelper.GetReport(ESignInStatus.Failure, nameof(Create), (int)ECode_Authenticate.用户不存在);
            //TODO
            //ESignInStatus.LockedOut
            //ESignInStatus.RequiresVerification
            if (string.IsNullOrEmpty(user.Password))
                return ReportHelper.GetReport(ESignInStatus.Failure, nameof(Create), (int)ECode_Authenticate.密码不可为空);

            var query = session.GetDbQueryBuilder().SelectBuilder;
            query.ComponentSelect.Add("1");
            query.ComponentWhere.Add(TUserProperties.UserName, user.UserName, LocateType.Equal);
            query.ComponentWhere.Add(TUserProperties.Password, user.Password, LocateType.Equal);
            if (session.GetQueryOperator().SelectAsInt<TUser>(query) != null)
                return new Report<ESignInStatus>(ESignInStatus.Success, CProtocol.CReport.CSuccess, "");
            else
                return ReportHelper.GetReport(ESignInStatus.Failure, nameof(Create), (int)ECode_Authenticate.操作数据库失败);
        }
        #endregion

        #region InRole
        public static Report IsInRole(this TUser user, DbSession session, List<ERole> roles)
        {
            user.FetchUserRoles(session);
            if (user.UserRoles.Count == 0)
            {
                if (roles.Contains(ERole.Guest))
                    return new Report(CProtocol.CReport.CSuccess);
                else
                    return new Report(CProtocol.CReport.CError);
            }
            if (user.UserRoles.FirstOrDefault(c => roles.Contains(c.RoleId)) != null)
                return new Report(CProtocol.CReport.CSuccess);
            else
                return new Report(CProtocol.CReport.CError);
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 检测用户是否存在
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        private static bool CheckExistance(this TUser user, DbSession session)
        {
            var query = session.GetDbQueryBuilder().SelectBuilder;
            query.ComponentSelect.Add("1");
            query.ComponentWhere.Add(TUserProperties.UserName,user.UserName, LocateType.Equal);
            var value = session.GetQueryOperator().SelectAsInt<TUser>(query);
            return value != null;
        }
        #endregion
    }
}
