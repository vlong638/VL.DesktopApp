using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using VL.Common.Constraints.ORM;
using VL.Common.ORM;
using System.Data;
using VL.User.Objects.Enums;

namespace VL.User.Objects.Entities
{
    [DataContract]
    public partial class TUserBasicInfo : IPDMTBase
    {
        #region Properties
        [DataMember]
        public String UserName { get; set; }
        [DataMember]
        public DateTime? Birthday { get; set; }
        [DataMember]
        public Int16? Mobile { get; set; }
        [DataMember]
        public String Email { get; set; }
        [DataMember]
        public String IdCardNumber { get; set; }
        #endregion

        #region Constructors
        public TUserBasicInfo()
        {
        }
        public TUserBasicInfo(String userName)
        {
            UserName = userName;
        }
        public TUserBasicInfo(IDataReader reader) : base(reader)
        {
        }
        #endregion

        #region Methods
        public override void Init(IDataReader reader)
        {
            this.UserName = Convert.ToString(reader[nameof(this.UserName)]);
            if (reader[nameof(this.Birthday)] != DBNull.Value)
            {
                this.Birthday = Convert.ToDateTime(reader[nameof(this.Birthday)]);
            }
            if (reader[nameof(this.Mobile)] != DBNull.Value)
            {
                this.Mobile = Convert.ToInt16(reader[nameof(this.Mobile)]);
            }
            if (reader[nameof(this.Email)] != DBNull.Value)
            {
                this.Email = Convert.ToString(reader[nameof(this.Email)]);
            }
            if (reader[nameof(this.IdCardNumber)] != DBNull.Value)
            {
                this.IdCardNumber = Convert.ToString(reader[nameof(this.IdCardNumber)]);
            }
        }
        public override void Init(IDataReader reader, List<string> fields)
        {
            if (fields.Contains(nameof(UserName)))
            {
                this.UserName = Convert.ToString(reader[nameof(this.UserName)]);
            }
            if (fields.Contains(nameof(Birthday)))
            {
                if (reader[nameof(this.Birthday)] != DBNull.Value)
                {
                    this.Birthday = Convert.ToDateTime(reader[nameof(this.Birthday)]);
                }
            }
            if (fields.Contains(nameof(Mobile)))
            {
                if (reader[nameof(this.Mobile)] != DBNull.Value)
                {
                    this.Mobile = Convert.ToInt16(reader[nameof(this.Mobile)]);
                }
            }
            if (fields.Contains(nameof(Email)))
            {
                if (reader[nameof(this.Email)] != DBNull.Value)
                {
                    this.Email = Convert.ToString(reader[nameof(this.Email)]);
                }
            }
            if (fields.Contains(nameof(IdCardNumber)))
            {
                if (reader[nameof(this.IdCardNumber)] != DBNull.Value)
                {
                    this.IdCardNumber = Convert.ToString(reader[nameof(this.IdCardNumber)]);
                }
            }
        }
        [DataMember]
        public override string TableName
        {
            get
            {
                return nameof(TUserBasicInfo);
            }
        }
        #endregion
    }
}
