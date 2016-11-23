using System;
using System.Collections.Generic;
using VL.Blog.Service.Utilities;
using VL.Common.Core.DAS;
using VL.Common.Core.Protocol;
using VL.Common.Object.VL.Blog;

namespace VL.Blog.Business
{
    /// <summary>
    /// Blog��Ҫ��
    /// ����,����,��ǩ
    /// ���������:1-1
    /// ����ͱ�ǩ:1:*
    /// </summary>
    public static class TBlogDomain
    {
        public static ClassReportHelper ReportHelper { set; get; } = ServiceContextOfBlog.ReportHelper.GetClassReportHelper(nameof(TBlogDomain));

        #region Edit
        public enum ECode_Edit
        {
            Default = CProtocol.CReport.CManualStart,
            �û�������Ϊ��,
            ���ⲻ��Ϊ��,
            ���ݲ���Ϊ��,
            �����½�ʧ��,
            �����½�ʧ��,
            ��ǩ�½�ʧ��,
            ���ݸ���ʧ��,
            �������ʧ��,
            ��ǩ����ʧ��,
        }
        public static Report Edit(this TBlog blog, DbSession session, string content, List<string> tags)
        {
            if (string.IsNullOrEmpty(blog.UserName))
                return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.�û�������Ϊ��);
            if (string.IsNullOrEmpty(blog.Title))
                return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.���ⲻ��Ϊ��);
            if (string.IsNullOrEmpty(content))
                return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.���ݲ���Ϊ��);

            blog.BreviaryContent = ContentHelper.GetBreviaryContent(content);
            blog.LastEditTime = DateTime.Now;
            blog.IsVisible = true;
            if (blog.BlogId != Guid.Empty)
            {
                if (!new TBlogDetail(blog.BlogId) { Content = content }.DbUpdate(session))
                    return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.���ݸ���ʧ��);
                if (blog.DbUpdate(session, TBlogProperties.BreviaryContent, TBlogProperties.LastEditTime, TBlogProperties.Title))
                    return new Report(CProtocol.CReport.CSuccess);
                else
                    return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.�������ʧ��);



            }
            else
            {
                blog.BlogId = Guid.NewGuid();
                blog.CreatedTime = DateTime.Now;
                if (!new TBlogDetail(blog.BlogId) { Content = content }.DbInsert(session))
                    return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.�����½�ʧ��);
                if (blog.DbInsert(session))
                    return new Report(CProtocol.CReport.CSuccess);
                else
                    return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.�����½�ʧ��);
                //TODO
                //foreach (var tag in tags)
                //{
                //    new TTag()
                //}
            }
        }
        #endregion

        #region Delete
        public enum ECode_Hide
        {
            Default = CProtocol.CReport.CManualStart,
            Id����Ϊ��,
            �������ʧ��,
        }
        public static Report ChangeVisibility(this TBlog blog, DbSession session)
        {
            if (blog.BlogId==Guid.Empty)
                return ReportHelper.GetReport(nameof(ChangeVisibility), (int)ECode_Hide.Id����Ϊ��);

            if (blog.DbUpdate(session,TBlogProperties.IsVisible))
                return new Report(CProtocol.CReport.CSuccess);
            else
                return ReportHelper.GetReport(nameof(ChangeVisibility), (int)ECode_Hide.�������ʧ��);
        }
        #endregion
    }
}
