using System;
using System.Collections.Generic;
using System.Linq;
using VL.Common.Constraints.Protocol;
using VL.Common.DAS;
using VL.Common.ORM;
using VL.Common.Protocol;
using VL.User.Objects.Enums;
using VL.User.Service.Utilities;

namespace VL.User.Objects.Entities
{
    public partial class TUser
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
        public Report Create(DbSession session)
        {
            if (string.IsNullOrEmpty(this.UserName))
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.�û�������Ϊ��);
            if (CheckExistance(session))
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.�û��Ѵ���);
            if (string.IsNullOrEmpty(this.Password))
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.���벻��Ϊ��);

            this.CreateTime = DateTime.Now;
            if (this.DbInsert(session))
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
        public Report<ESignInStatus> Authenticate(DbSession session)
        {
            if (string.IsNullOrEmpty(this.UserName))
                return ReportHelper.GetReport(ESignInStatus.Failure, nameof(Create), (int)ECode_Authenticate.�û�������Ϊ��);
            if (!CheckExistance(session))
                return ReportHelper.GetReport(ESignInStatus.Failure, nameof(Create), (int)ECode_Authenticate.�û�������);
            //TODO
            //ESignInStatus.LockedOut
            //ESignInStatus.RequiresVerification
            if (string.IsNullOrEmpty(this.Password))
                return ReportHelper.GetReport(ESignInStatus.Failure, nameof(Create), (int)ECode_Authenticate.���벻��Ϊ��);

            var query = session.GetDbQueryBuilder().SelectBuilder;
            query.ComponentSelect.Add("1");
            query.ComponentWhere.Add(TUserProperties.UserName, this.UserName, LocateType.Equal);
            query.ComponentWhere.Add(TUserProperties.Password, this.Password, LocateType.Equal);
            if (session.GetQueryOperator().SelectAsInt<TUser>(session, query) != null)
                return new Report<ESignInStatus>(ESignInStatus.Success, CProtocol.CReport.CSuccess, "");
            else
                return ReportHelper.GetReport(ESignInStatus.Failure, nameof(Create), (int)ECode_Authenticate.�������ݿ�ʧ��);
        }
        #endregion

        #region InRole
        public Report IsInRole(DbSession session, List<ERole> roles)
        {
            this.FetchUserRoles(session);
            if (this.UserRoles.Count == 0)
            {
                if (roles.Contains(ERole.Guest))
                    return new Report(CProtocol.CReport.CSuccess);
                else
                    return new Report(CProtocol.CReport.CError);
            }
            if (this.UserRoles.FirstOrDefault(c => roles.Contains(c.RoleId)) != null)
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
        private bool CheckExistance(DbSession session)
        {
            var query = session.GetDbQueryBuilder().SelectBuilder;
            query.ComponentSelect.Add("1");
            query.ComponentWhere.Add(TUserProperties.UserName, UserName, LocateType.Equal);
            var value = session.GetQueryOperator().SelectAsInt<TUser>(session, query);
            return value != null;
        }
        #endregion
    }
}
