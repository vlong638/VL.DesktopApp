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
            �û�������Ϊ��,
            �û��Ѵ���,
            ���벻��Ϊ��,
            �������ݿ�ʧ��,
        }
        public static Report Create(this TUser user,DbSession session)
        {
            if (string.IsNullOrEmpty(user.UserName))
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.�û�������Ϊ��);
            if (user.CheckExistance(session))
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.�û��Ѵ���);
            if (string.IsNullOrEmpty(user.Password))
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.���벻��Ϊ��);

            user.CreateTime = DateTime.Now;
            if (user.DbInsert(session))
                return new Report(CProtocol.CReport.CSuccess);
            else
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.�������ݿ�ʧ��);
        }
        #endregion

        #region Regist
        enum ECode_Authenticate
        {
            Default = CProtocol.CReport.CManualStart,
            �û�������Ϊ��,
            �û�������,
            ���벻��Ϊ��,
            �������ݿ�ʧ��,
        }
        public static Report<ESignInStatus> Authenticate(this TUser user, DbSession session)
        {
            if (string.IsNullOrEmpty(user.UserName))
                return ReportHelper.GetReport(ESignInStatus.Failure, nameof(Create), (int)ECode_Authenticate.�û�������Ϊ��);
            if (!user.CheckExistance(session))
                return ReportHelper.GetReport(ESignInStatus.Failure, nameof(Create), (int)ECode_Authenticate.�û�������);
            //TODO
            //ESignInStatus.LockedOut
            //ESignInStatus.RequiresVerification
            if (string.IsNullOrEmpty(user.Password))
                return ReportHelper.GetReport(ESignInStatus.Failure, nameof(Create), (int)ECode_Authenticate.���벻��Ϊ��);

            var query = session.GetDbQueryBuilder().SelectBuilder;
            query.ComponentSelect.Add("1");
            query.ComponentWhere.Add(TUserProperties.UserName, user.UserName, LocateType.Equal);
            query.ComponentWhere.Add(TUserProperties.Password, user.Password, LocateType.Equal);
            if (session.GetQueryOperator().SelectAsInt<TUser>(query) != null)
                return new Report<ESignInStatus>(ESignInStatus.Success, CProtocol.CReport.CSuccess, "");
            else
                return ReportHelper.GetReport(ESignInStatus.Failure, nameof(Create), (int)ECode_Authenticate.�������ݿ�ʧ��);
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

        #region ��������
        /// <summary>
        /// ����û��Ƿ����
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
