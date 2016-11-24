using System;
using System.Collections.Generic;
using System.Linq;
using VL.Blog.Service.Utilities;
using VL.Common.Core.DAS;
using VL.Common.Core.Protocol;
using VL.Common.Core.Object.VL.Blog;

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
            ���ݸ���ʧ��,
            �������ʧ��,
            ��ǩ�½�ʧ��,
            ��ǩɾ��ʧ��,
            ��������ʧ��,
            �����½�ʧ��,
            ����ɾ��ʧ��,
        }
        public static Report Edit(this TBlog blog, DbSession session, string content, List<string> addTags, List<string> deleteTags)
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
                //Detail
                if (!new TBlogDetail(blog.BlogId) { Content = content }.DbUpdate(session))
                    return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.���ݸ���ʧ��);
                //Body
                if (!blog.DbUpdate(session, TBlogProperties.BreviaryContent, TBlogProperties.LastEditTime, TBlogProperties.Title))
                    return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.�������ʧ��);
                //Added Tag
                foreach (var tagName in addTags)
                {
                    //Tag
                    var tag = new TTag(blog.UserName, tagName);
                    if (tag.DbSelect(session)==null)
                    {
                        if (!tag.DbInsert(session))
                        {
                            return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.��ǩ�½�ʧ��);
                        }
                    }
                    //BlogTagMapper
                    if (!new TBlogTagMapper( blog.BlogId, tag.TagName).DbInsert(session))
                    {
                        return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.�����½�ʧ��);
                    }
                }
                //Deleted Tag
                blog.FetchBlogTagMappers(session);
                foreach (var tagName in deleteTags)
                {
                    //BlogTagMapper
                    var blogTag = blog.BlogTagMappers.FirstOrDefault(c => c.TagName == tagName);
                    if (blogTag==null)
                    {
                        return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.��������ʧ��);
                    }
                    if (!new TBlogTagMapper(blog.BlogId, blogTag.TagName).DbDelete(session))
                    {
                        return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.����ɾ��ʧ��);
                    }
                    //Tag
                    if (!blogTag.IsBlogHasTags(session))
                    {
                        if (!new TTag(blog.UserName,blogTag.TagName).DbDelete(session))
                        {
                            return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.��ǩɾ��ʧ��);
                        }
                    }
                }
            }
            else
            {
                blog.BlogId = Guid.NewGuid();
                blog.CreatedTime = DateTime.Now;
                //Detail
                if (!new TBlogDetail(blog.BlogId) { Content = content }.DbInsert(session))
                    return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.�����½�ʧ��);
                //Body
                if (!blog.DbInsert(session))
                    return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.�����½�ʧ��);
                foreach (var tagName in addTags)
                {
                    //Tag
                    var tag = new TTag(blog.UserName, tagName);
                    if (tag.DbSelect(session) == null)
                    {
                        if (!tag.DbInsert(session))
                        {
                            return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.��ǩ�½�ʧ��);
                        }
                    }
                    //BlogTagMapper
                    if (!new TBlogTagMapper(blog.BlogId, tag.TagName).DbInsert(session))
                    {
                        return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.�����½�ʧ��);
                    }
                }
            }
            return new Report(CProtocol.CReport.CSuccess);
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
