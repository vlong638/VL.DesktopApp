using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VL.ItsMe1110.Models
{
    public class BlogListViewItem
    {
        public Guid BlogId { get; set; } = Guid.Empty;

        [Display(Name = "标题")]
        public string Title { get; set; }

        [Display(Name = "缩略内容")]
        public string BreviaryContent { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreatedTime { get; set; }

        [Display(Name = "最后编辑时间")]
        public DateTime LastEditTime { get; set; }

        [Display(Name = "是否可见")]
        public bool IsVisible { get; set; }
    }
    public class BlogListViewModel : List<BlogListViewItem>
    {
    }
    public class BlogDetailViewModel
    {
        public Guid BlogId { get; set; } = Guid.Empty;

        [Display(Name = "作者")]
        public string UserName { get; set; }

        [Display(Name = "标题")]
        public string Title { get; set; }

        [Display(Name = "内容")]
        public string Content { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreatedTime { get; set; }

        [Display(Name = "最后编辑时间")]
        public DateTime LastEditTime { get; set; }
    }
    public class BlogEditModel
    {
        public Guid BlogId { get; set; } = Guid.Empty;

        [Display(Name = "标题")]
        //[Required]
        public string Title { get; set; }

        [Display(Name = "内容")]
        public string Content { get; set; }
    }
}