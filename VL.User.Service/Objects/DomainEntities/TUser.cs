using System;
using VL.Common.DAS;
using VL.Common.ORM;
using VL.Common.Protocol;

namespace VL.User.Objects.Entities
{
    public partial class TUser
    {
        static ClassReportHelper ReportHelper { set; get; } = Constraints.ReportHelper.GetClassReportHelper(nameof(TUser));

        #region �����û�
        enum ECode_Create
        {
            Default = Report.CodeOfManualStart,
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
            if (CheckExistance(session))
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
        #endregion

        #region �û���֤
        enum ECode_Authenticate
        {
            Default = Report.CodeOfManualStart,
            �û�������Ϊ��,
            �û�������,
            ���벻��Ϊ��,
            �������ݿ�ʧ��,
        }
        public Report Authenticate(DbSession session)
        {
            if (string.IsNullOrEmpty(this.UserName))
            {
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Authenticate.�û�������Ϊ��);
            }
            if (!CheckExistance(session))
            {
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Authenticate.�û�������);
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Authenticate.���벻��Ϊ��);
            }
            var query = session.GetDbQueryBuilder().SelectBuilder;
            query.ComponentSelect.Add("1");
            query.ComponentWhere.Add(TUserProperties.UserName, this.UserName, LocateType.Equal);
            query.ComponentWhere.Add(TUserProperties.Password, this.Password, LocateType.Equal);
            if (session.GetQueryOperator().SelectAsInt<TUser>(session, query) != null)
                return new Report(Report.CodeOfSuccess, "");
            else
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Authenticate.�������ݿ�ʧ��);
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
