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
            标签新建失败,
            标签删除失败,
            关联加载失败,
            关联新建失败,
            关联删除失败,
        }
        public static Report Edit(this TBlog blog, DbSession session, string content, List<string> addTags, List<string> deleteTags)
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
                //Detail
                if (!new TBlogDetail(blog.BlogId) { Content = content }.DbUpdate(session))
                    return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.内容更新失败);
                //Body
                if (!blog.DbUpdate(session, TBlogProperties.BreviaryContent, TBlogProperties.LastEditTime, TBlogProperties.Title))
                    return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.主体更新失败);
                //Added Tag
                foreach (var tagName in addTags)
                {
                    //Tag
                    var tag = new TTag(blog.UserName, tagName);
                    if (tag.DbSelect(session)==null)
                    {
                        if (!tag.DbInsert(session))
                        {
                            return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.标签新建失败);
                        }
                    }
                    //BlogTagMapper
                    if (!new TBlogTagMapper( blog.BlogId, tag.TagName).DbInsert(session))
                    {
                        return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.关联新建失败);
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
                        return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.关联加载失败);
                    }
                    if (!new TBlogTagMapper(blog.BlogId, blogTag.TagName).DbDelete(session))
                    {
                        return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.关联删除失败);
                    }
                    //Tag
                    if (!blogTag.IsBlogHasTags(session))
                    {
                        if (!new TTag(blog.UserName,blogTag.TagName).DbDelete(session))
                        {
                            return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.标签删除失败);
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
                    return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.内容新建失败);
                //Body
                if (!blog.DbInsert(session))
                    return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.主体新建失败);
                foreach (var tagName in addTags)
                {
                    //Tag
                    var tag = new TTag(blog.UserName, tagName);
                    if (tag.DbSelect(session) == null)
                    {
                        if (!tag.DbInsert(session))
                        {
                            return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.标签新建失败);
                        }
                    }
                    //BlogTagMapper
                    if (!new TBlogTagMapper(blog.BlogId, tag.TagName).DbInsert(session))
                    {
                        return ReportHelper.GetReport(nameof(Edit), (int)ECode_Edit.关联新建失败);
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
