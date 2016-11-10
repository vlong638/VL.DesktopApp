using System;
using VL.Common.DAS.Objects;
using VL.Common.ORM.Utilities.QueryBuilders;
using VL.Common.Protocol.IService;

namespace VL.User.Objects.Entities
{
    public partial class TUser
    {
        static ClassReportHelper ReportHelper { set; get; } = Constraints.ReportHelper.GetClassReportHelper(nameof(TUser));

        enum ECode_Create
        {
            Default=Report.CodeOfManualStart,
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
            var query= IORMProvider.GetDbQueryBuilder(session).SelectBuilder;
            query.ComponentSelect.Add("1");
            query.ComponentWhere.Add(TUserProperties.UserName, UserName, LocateType.Equal);
            var @operator = IORMProvider.GetQueryOperator(session);
            var value = @operator.SelectAsInt<TUser>(session, query);
            if (value!=null)
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
        enum ECode_Authenticate
        {
            Default = Report.CodeOfManualStart,
            用户名_空值检测,
            密码_空值检测,
            操作数据库失败,
        }
        public Report Authenticate(DbSession session)
        {
            if (string.IsNullOrEmpty(this.UserName))
            {
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.用户名不可为空);
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.密码不可为空);
            }
            var builder = IORMProvider.GetDbQueryBuilder(session).SelectBuilder;
            builder.ComponentWhere.Add(TUserProperties.UserName, this.UserName, LocateType.Equal);
            builder.ComponentWhere.Add(TUserProperties.UserName, this.Password, LocateType.Equal);
            if (this.DbSelect(session, builder) !=null)
                return new Report(Report.CodeOfSuccess, "");
            else
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.操作数据库失败);
        }
    }
}
