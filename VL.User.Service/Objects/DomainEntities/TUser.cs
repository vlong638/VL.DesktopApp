using System;
using VL.Common.DAS;
using VL.Common.ORM;
using VL.Common.Protocol;

namespace VL.User.Objects.Entities
{
    public partial class TUser
    {
        static ClassReportHelper ReportHelper { set; get; } = Constraints.ReportHelper.GetClassReportHelper(nameof(TUser));

        #region 创建用户
        enum ECode_Create
        {
            Default = Report.CodeOfManualStart,
            用户名不可为空,
            用户已存在,
            密码不可为空,
            操作数据库失败,
        }
        public Report Create(DbSession session)
        {
            this.CreateTime = DateTime.Now;
            if (string.IsNullOrEmpty(this.UserName))
            {
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.用户名不可为空);
            }
            if (CheckExistance(session))
            {
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.用户已存在);
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.密码不可为空);
            }
            if (this.DbInsert(session))
                return new Report(Report.CodeOfSuccess, "");
            else
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.操作数据库失败);
        } 
        #endregion

        #region 用户验证
        enum ECode_Authenticate
        {
            Default = Report.CodeOfManualStart,
            用户名不可为空,
            用户不存在,
            密码不可为空,
            操作数据库失败,
        }
        public Report Authenticate(DbSession session)
        {
            if (string.IsNullOrEmpty(this.UserName))
            {
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Authenticate.用户名不可为空);
            }
            if (!CheckExistance(session))
            {
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Authenticate.用户不存在);
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Authenticate.密码不可为空);
            }
            var query = session.GetDbQueryBuilder().SelectBuilder;
            query.ComponentSelect.Add("1");
            query.ComponentWhere.Add(TUserProperties.UserName, this.UserName, LocateType.Equal);
            query.ComponentWhere.Add(TUserProperties.Password, this.Password, LocateType.Equal);
            if (session.GetQueryOperator().SelectAsInt<TUser>(session, query) != null)
                return new Report(Report.CodeOfSuccess, "");
            else
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Authenticate.操作数据库失败);
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
