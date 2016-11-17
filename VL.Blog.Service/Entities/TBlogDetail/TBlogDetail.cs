using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using VL.Common.ORM;
using VL.Blog.Objects.Enums;

namespace VL.Blog.Objects.Entities
{
    [DataContract]
    public partial class TBlogDetail : IPDMTBase
    {
        #region Properties
        [DataMember]
        public Guid BlogId { get; set; }
        [DataMember]
        public String Content { get; set; }
        #endregion

        #region Constructors
        public TBlogDetail()
        {
        }
        public TBlogDetail(Guid blogId)
        {
            BlogId = blogId;
        }
        public TBlogDetail(IDataReader reader) : base(reader)
        {
        }
        #endregion

        #region Methods
        public override void Init(IDataReader reader)
        {
            this.BlogId = new Guid(reader[nameof(this.BlogId)].ToString());
            this.Content = Convert.ToString(reader[nameof(this.Content)]);
        }
        public override void Init(IDataReader reader, List<string> fields)
        {
            if (fields.Contains(nameof(BlogId)))
            {
                this.BlogId = new Guid(reader[nameof(this.BlogId)].ToString());
            }
            if (fields.Contains(nameof(Content)))
            {
                this.Content = Convert.ToString(reader[nameof(this.Content)]);
            }
        }
        [DataMember]
        public override string TableName
        {
            get
            {
                return nameof(TBlogDetail);
            }
        }
        #endregion
    }
}
