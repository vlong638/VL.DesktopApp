using System;
using System.Collections.Generic;
using System.Linq;
using VL.Common.Constraints;
using VL.Common.DAS;
using VL.Common.ORM;
using VL.Common.Protocol;
using VL.User.Objects.Enums;

namespace VL.User.Objects.Entities
{
    public partial class TUser
    {
        static ClassReportHelper ReportHelper { set; get; } = Constraints.ReportHelper.GetClassReportHelper(nameof(TUser));

        #region 创建用户
        enum ECode_Create
        {
            Default = CProtocol.CReport.CManualStart,
            用户名不可为空,
            用户已存在,
            密码不可为空,
            操作数据库失败,
        }
        public Report Create(DbSession session)
        {
            this.CreateTime = DateTime.Now;
            if (string.IsNullOrEmpty(this.UserName))
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.用户名不可为空);
            if (CheckExistance(session))
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.用户已存在);
            if (string.IsNullOrEmpty(this.Password))
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.密码不可为空);
            if (this.DbInsert(session))
                return new Report(CProtocol.CReport.CSuccess);
            else
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.操作数据库失败);
        }
        #endregion

        #region 用户验证
        enum ECode_Authenticate
        {
            Default = CProtocol.CReport.CManualStart,
            用户名不可为空,
            用户不存在,
            密码不可为空,
            操作数据库失败,
        }
        public Report<ESignInStatus> Authenticate(DbSession session)
        {
            if (string.IsNullOrEmpty(this.UserName))
                return ReportHelper.GetReport(ESignInStatus.Failure, nameof(Create), (int)ECode_Authenticate.用户名不可为空);
            if (!CheckExistance(session))
                return ReportHelper.GetReport(ESignInStatus.Failure, nameof(Create), (int)ECode_Authenticate.用户不存在);
            //TODO
            //ESignInStatus.LockedOut
            //ESignInStatus.RequiresVerification
            if (string.IsNullOrEmpty(this.Password))
                return ReportHelper.GetReport(ESignInStatus.Failure, nameof(Create), (int)ECode_Authenticate.密码不可为空);
            var query = session.GetDbQueryBuilder().SelectBuilder;
            query.ComponentSelect.Add("1");
            query.ComponentWhere.Add(TUserProperties.UserName, this.UserName, LocateType.Equal);
            query.ComponentWhere.Add(TUserProperties.Password, this.Password, LocateType.Equal);
            if (session.GetQueryOperator().SelectAsInt<TUser>(session, query) != null)
                return new Report<ESignInStatus>(ESignInStatus.Success, CProtocol.CReport.CSuccess, "");
            else
                return ReportHelper.GetReport(ESignInStatus.Failure, nameof(Create), (int)ECode_Authenticate.操作数据库失败);
        }
        #endregion

        #region 用户角色验证
        public Report IsInRole(DbSession session, string[] roles)
        {
            this.FetchUserRoles(session);
            if (UserRoles.Count == 0)
            {
                if (roles.Contains(ERoles.Guest.ToString()))
                    return new Report(CProtocol.CReport.CSuccess);
                else
                    return new Report(CProtocol.CReport.CError);
            }
            List<int> roleIds = new List<int>();
            foreach (var role in roles)
            {
                roleIds.Add((int)((ERoles)Enum.Parse(typeof(ERoles), role)));
            }
            if (UserRoles.FirstOrDefault(c => roleIds.Contains((int)c.RoleId)) != null)
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
