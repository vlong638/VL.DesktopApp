using System;
using VL.Blog.Service.Utilities;
using VL.Common.DAS;
using VL.Common.Object.Protocol;
using VL.Common.Object.VL.Blog;
using VL.Common.Protocol;

namespace VL.Blog.Business
{
    /// <summary>
    /// Blog三要素
    /// 主体,内容,标签
    /// 主体和内容:1-1
    /// 主体和标签:1:*
    /// </summary>
    public static class TBlogDomain
    {
        public static ClassReportHelper ReportHelper { set; get; } = ServiceContextOfBlog.ReportHelper.GetClassReportHelper(nameof(TBlogDomain));

        #region Edit
        public enum ECode_Edit
        {
            Default = CProtocol.CReport.CManualStart,
            用户名不可为空,
            标题不可为空,
            内容不可为空,
            内容新建失败,
            主体新建失败,
            内容更新失败,
            主体更新失败,
        }
        public static Report Edit(this TBlog blog, DbSession session, string content)
        {
            if (string.IsNullOrEmpty(blog.UserName))
                return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.用户名不可为空);
            if (string.IsNullOrEmpty(blog.Title))
                return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.标题不可为空);
            if (string.IsNullOrEmpty(content))
                return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.内容不可为空);

            blog.BreviaryContent = ContentHelper.GetBreviaryContent(content);
            blog.LastEditTime = DateTime.Now;
            blog.IsVisible = true;
            if (blog.BlogId != Guid.Empty)
            {
                if (!new TBlogDetail(blog.BlogId) { Content = content }.DbUpdate(session))
                    return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.内容更新失败);
                if (blog.DbUpdate(session, TBlogProperties.BreviaryContent, TBlogProperties.LastEditTime, TBlogProperties.Title))
                    return new Report(CProtocol.CReport.CSuccess);
                else
                    return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.主体更新失败);
            }
            else
            {
                blog.BlogId = Guid.NewGuid();
                blog.CreatedTime = DateTime.Now;
                if (!new TBlogDetail(blog.BlogId) { Content = content }.DbInsert(session))
                    return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.内容新建失败);
                if (blog.DbInsert(session))
                    return new Report(CProtocol.CReport.CSuccess);
                else
                    return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.主体新建失败);
            }
        }
        #endregion

        #region Delete
        public enum ECode_Hide
        {
            Default = CProtocol.CReport.CManualStart,
            Id不可为空,
            主体更新失败,
        }
        public static Report ChangeVisibility(this TBlog blog, DbSession session)
        {
            if (blog.BlogId==Guid.Empty)
                return ReportHelper.GetReport(nameof(ChangeVisibility), (int)ECode_Hide.Id不可为空);

            if (blog.DbUpdate(session,TBlogProperties.IsVisible))
                return new Report(CProtocol.CReport.CSuccess);
            else
                return ReportHelper.GetReport(nameof(ChangeVisibility), (int)ECode_Hide.主体更新失败);
        }
        #endregion
    }
}
