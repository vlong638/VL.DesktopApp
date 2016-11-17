using System;
using System.Collections.Generic;
using System.Linq;
using VL.Blog.Service.Utilities;
using VL.Common.DAS;
using VL.Common.Object.Protocol;
using VL.Common.Object.VL.Blog;
using VL.Common.ORM;
using VL.Common.Protocol;

namespace VL.Blog.Business
{
    public static class TBlogDomain
    {
        static ClassReportHelper ReportHelper { set; get; } = ServiceContextOfBlog.ReportHelper.GetClassReportHelper(nameof(TBlog));

        #region Create
        enum ECode_Create
        {
            Default = CProtocol.CReport.CManualStart,
            �û�������Ϊ��,
            ���ⲻ��Ϊ��,
            ���ݲ�������ʮ���ַ�,
            ���ݱ���ʧ��,
            ���屣��ʧ��,
        }
        public static Report Create(this TBlog blog, DbSession session, string content)
        {
            if (string.IsNullOrEmpty(blog.UserName))
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.�û�������Ϊ��);
            if (string.IsNullOrEmpty(blog.Title))
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.���ⲻ��Ϊ��);
            if (content == null || content.Length < 10)
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.���ݲ�������ʮ���ַ�);

            blog.BlogId = Guid.NewGuid();
            blog.BreviaryContent = ContentHelper.GetBreviaryContent(content);
            blog.CreatedTime = DateTime.Now;
            blog.LastEditTime = DateTime.Now;
            #region Detail
            TBlogDetail detail = new TBlogDetail()
            {
                BlogId = Guid.NewGuid(),
                Content = content,
            };
            if (!detail.DbInsert(session))
            {
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.���ݱ���ʧ��);
            }
            #endregion
            if (blog.DbInsert(session))
                return new Report(CProtocol.CReport.CSuccess);
            else
                return ReportHelper.GetReport(nameof(Create), (int)ECode_Create.���屣��ʧ��);
        }
        #endregion
    }
}
