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
            �û�������Ϊ��,
            �û��Ѵ���,
            ���벻��Ϊ��,
            �������ݿ�ʧ��,
        }
        public Report Create(DbSession session)
        {
            this.CreateTime = DateTime.Now;
            if (string.IsNullOrEmpty(this.UserName))
            {
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.�û�������Ϊ��);
            }
            var query= IORMProvider.GetDbQueryBuilder(session).SelectBuilder;
            query.ComponentSelect.Add("1");
            query.ComponentWhere.Add(TUserProperties.UserName, UserName, LocateType.Equal);
            var @operator = IORMProvider.GetQueryOperator(session);
            var value = @operator.SelectAsInt<TUser>(session, query);
            if (value!=null)
            {
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.�û��Ѵ���);
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.���벻��Ϊ��);
            }
            if (this.DbInsert(session))
                return new Report(Report.CodeOfSuccess, "");
            else
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.�������ݿ�ʧ��);
        }
        enum ECode_Authenticate
        {
            Default = Report.CodeOfManualStart,
            �û���_��ֵ���,
            ����_��ֵ���,
            �������ݿ�ʧ��,
        }
        public Report Authenticate(DbSession session)
        {
            if (string.IsNullOrEmpty(this.UserName))
            {
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.�û�������Ϊ��);
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.���벻��Ϊ��);
            }
            var builder = IORMProvider.GetDbQueryBuilder(session).SelectBuilder;
            builder.ComponentWhere.Add(TUserProperties.UserName, this.UserName, LocateType.Equal);
            builder.ComponentWhere.Add(TUserProperties.UserName, this.Password, LocateType.Equal);
            if (this.DbSelect(session, builder) !=null)
                return new Report(Report.CodeOfSuccess, "");
            else
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.�������ݿ�ʧ��);
        }
    }
}
