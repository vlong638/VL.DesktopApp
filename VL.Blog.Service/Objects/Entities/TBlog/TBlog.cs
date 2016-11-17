using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using VL.Common.Object.ORM;
using VL.Common.ORM;
using System.Data;
using VL.Blog.Objects.Enums;

namespace VL.Blog.Objects.Entities
{
    [DataContract]
    public partial class TBlog : IPDMTBase
    {
        #region Properties
        [DataMember]
        public String UserName { get; set; }
        [DataMember]
        public Guid BlogId { get; set; }
        [DataMember]
        public String Title { get; set; }
        [DataMember]
        public String BreviaryContent { get; set; }
        [DataMember]
        public DateTime CreatedTime { get; set; }
        [DataMember]
        public DateTime LastEditTime { get; set; }
        #endregion

        #region Constructors
        public TBlog()
        {
        }
        public TBlog(Guid blogId)
        {
            BlogId = blogId;
        }
        public TBlog(IDataReader reader) : base(reader)
        {
        }
        #endregion

        #region Methods
        public override void Init(IDataReader reader)
        {
            this.UserName = Convert.ToString(reader[nameof(this.UserName)]);
            this.BlogId = new Guid(reader[nameof(this.BlogId)].ToString());
            this.Title = Convert.ToString(reader[nameof(this.Title)]);
            this.BreviaryContent = Convert.ToString(reader[nameof(this.BreviaryContent)]);
            this.CreatedTime = Convert.ToDateTime(reader[nameof(this.CreatedTime)]);
            this.LastEditTime = Convert.ToDateTime(reader[nameof(this.LastEditTime)]);
        }
        public override void Init(IDataReader reader, List<string> fields)
        {
            if (fields.Contains(nameof(UserName)))
            {
                this.UserName = Convert.ToString(reader[nameof(this.UserName)]);
            }
            if (fields.Contains(nameof(BlogId)))
            {
                this.BlogId = new Guid(reader[nameof(this.BlogId)].ToString());
            }
            if (fields.Contains(nameof(Title)))
            {
                this.Title = Convert.ToString(reader[nameof(this.Title)]);
            }
            if (fields.Contains(nameof(BreviaryContent)))
            {
                this.BreviaryContent = Convert.ToString(reader[nameof(this.BreviaryContent)]);
            }
            if (fields.Contains(nameof(CreatedTime)))
            {
                this.CreatedTime = Convert.ToDateTime(reader[nameof(this.CreatedTime)]);
            }
            if (fields.Contains(nameof(LastEditTime)))
            {
                this.LastEditTime = Convert.ToDateTime(reader[nameof(this.LastEditTime)]);
            }
        }
        [DataMember]
        public override string TableName
        {
            get
            {
                return nameof(TBlog);
            }
        }
        #endregion
    }
}
