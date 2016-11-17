using System;
using VL.Blog.Service.Utilities;
using VL.Common.Constraints.Protocol;
using VL.Common.DAS;
using VL.Common.Protocol;

namespace VL.Blog.Objects.Entities
{
    public partial class TBlog
    {
        static ClassReportHelper ReportHelper { set; get; } = ServiceContextOfBlog.ReportHelper.GetClassReportHelper(nameof(TBlog));

        #region Create
        enum ECode_Create
        {
            Default = CProtocol.CReport.CManualStart,
            用户名不可为空,
            标题不可为空,
            内容不可少于十个字符,
            内容保存失败,
            主体保存失败,
        }
        public Report Create(DbSession session, string content)
        {
            if (string.IsNullOrEmpty(this.UserName))
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.用户名不可为空);
            if (string.IsNullOrEmpty(this.Title))
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.标题不可为空);
            if (content == null || content.Length < 10)
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.内容不可少于十个字符);

            this.BlogId = Guid.NewGuid();
            this.BreviaryContent = ContentHelper.GetBreviaryContent(content);
            this.CreatedTime = DateTime.Now;
            this.LastEditTime = DateTime.Now;
            #region Detail
            TBlogDetail detail = new TBlogDetail()
            {
                BlogId = Guid.NewGuid(),
                Content = content,
            };
            if (!detail.DbInsert(session))
            {
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.内容保存失败);
            }
            #endregion
            if (this.DbInsert(session))
                return new Report(CProtocol.CReport.CSuccess);
            else
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.主体保存失败);
        }
        #endregion
    }
}
