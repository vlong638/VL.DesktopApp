using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VL.Common.Object.Protocol;
using VL.Common.Object.VL.Blog;
using VL.ItsMe1110.Custom.Authentications;
using VL.ItsMe1110.Models;
using VL.ItsMe1110.ObjectBlogService;
using VL.ItsMe1110.SubjectBlogService;

namespace VL.ItsMe1110.Controllers
{
    public class BlogController : BaseController
    {
        #region 服务
        private SubjectBlogServiceClient _subjectBlogClient;
        private ObjectBlogServiceClient _ObjectBlogClient;
        public SubjectBlogServiceClient SubjectBlogClient
        {
            get
            {
                if (_subjectBlogClient == null)
                {
                    _subjectBlogClient = new SubjectBlogServiceClient();
                }
                return _subjectBlogClient;
            }
        }
        public ObjectBlogServiceClient ObjectBlogClient
        {
            get
            {
                if (_ObjectBlogClient == null)
                {
                    _ObjectBlogClient = new ObjectBlogServiceClient();
                }
                return _ObjectBlogClient;
            }
        }
        #endregion

        [HttpGet]
        [VLAuthorize(Users = VESTEDUSERSTRING)]
        public ActionResult Index()
        {
            var blogs = ObjectBlogClient.GetAllBlogs();
            var model = new BlogListViewModel();
            foreach (var blog in blogs.Data)
            {
                model.Add(new BlogListViewItem()
                {
                    BlogId = blog.BlogId,
                    Title = blog.Title,
                    BreviaryContent = blog.BreviaryContent,
                    CreatedTime = blog.CreatedTime,
                    LastEditTime = blog.LastEditTime,
                    IsVisible = blog.IsVisible,
                });
            }
            return View(model);
        }

        [HttpGet]
        [VLAuthorize(Users = VESTEDUSERSTRING)]
        public async Task<ActionResult> Edit(Guid id, string title)
        {
            if (id == Guid.Empty)
            {
                return View(new BlogEditModel() { BlogId = id });
            }

            var result = await ObjectBlogClient.GetBlogDetailAsync(id);
            if (result.Code == CProtocol.CReport.CSuccess)
            {
                return View(new BlogEditModel() { BlogId = id, Title = title, Content = result.Data.Content });
            }
            else
            {
                AddMessages(result.Code, new KeyValueCollection {
                            new KeyValue(2, "系统内部异常") ,
                        });
                return View(new BlogEditModel() { BlogId = id });
            }
        }

        [HttpPost]
        [VLAuthorize(Users = VESTEDUSERSTRING)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BlogEditModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await SubjectBlogClient.EditBlogAsync(new TBlog(model.BlogId) { UserName = User.Identity.Name, Title = model.Title }, model.Content);
            if (result.Code == CProtocol.CReport.CSuccess)
            {
                return RedirectToAction(nameof(BlogController.Index), PAGENAME_BLOG);
            }
            else
            {
                AddMessages(result.Code, new KeyValueCollection {
                            new KeyValue(2, "系统内部异常") ,
                            new KeyValue(11, "用户名不可为空") ,
                            new KeyValue(12, "标题不可为空") ,
                            new KeyValue(13, "内容不可少于十个字符") ,
                            new KeyValue(14, "内容新建失败") ,
                            new KeyValue(15, "主体新建失败") ,
                            new KeyValue(16, "内容更新失败") ,
                            new KeyValue(17, "主体更新失败") ,
                        });
                return View(model);
            }
        }

        [HttpPost]
        [VLAuthorize(Users = VESTEDUSERSTRING)]
        public async Task<bool> ChangeVisibility(Guid id, bool isVisible)
        {
            var result = await SubjectBlogClient.ChangeVisibilityAsync(id,isVisible);
            if (result.Code != CProtocol.CReport.CSuccess)
            {
                //AddMessages(result.Code, new KeyValueCollection {
                //            new KeyValue(2, "系统内部异常") ,
                //            new KeyValue(11, "Id不可为空") ,
                //            new KeyValue(12, "主体更新失败") ,
                //        });
                return false;
            }
            return true;
            //return RedirectToAction(nameof(BlogController.Index), PAGENAME_BLOG);
        }
    }
}