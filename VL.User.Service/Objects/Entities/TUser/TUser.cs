using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using VL.Common.ORM.Objects;
using VL.User.Objects.Enums;

namespace VL.User.Objects.Entities
{
    [DataContract]
    public partial class TUser : IPDMTBase
    {
        #region Properties
        [DataMember]
        public String UserName { get; set; }
        [DataMember]
        public String Password { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        #endregion

        #region Constructors
        public TUser()
        {
        }
        public TUser(String userName)
        {
            UserName = userName;
        }
        public TUser(IDataReader reader) : base(reader)
        {
        }
        #endregion

        #region Methods
        public override void Init(IDataReader reader)
        {
            this.UserName = Convert.ToString(reader[nameof(this.UserName)]);
            this.Password = Convert.ToString(reader[nameof(this.Password)]);
            this.CreateTime = Convert.ToDateTime(reader[nameof(this.CreateTime)]);
        }
        public override void Init(IDataReader reader, List<string> fields)
        {
            if (fields.Contains(nameof(UserName)))
            {
                this.UserName = Convert.ToString(reader[nameof(this.UserName)]);
            }
            if (fields.Contains(nameof(Password)))
            {
                this.Password = Convert.ToString(reader[nameof(this.Password)]);
            }
            if (fields.Contains(nameof(CreateTime)))
            {
                this.CreateTime = Convert.ToDateTime(reader[nameof(this.CreateTime)]);
            }
        }
        [DataMember]
        public override string TableName
        {
            get
            {
                return nameof(TUser);
            }
        }
        #endregion
    }
}
