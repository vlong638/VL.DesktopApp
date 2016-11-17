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
    public partial class TUserRole : IPDMTBase
    {
        #region Properties
        [DataMember]
        public String UserName { get; set; }
        [DataMember]
        public ERole RoleId { get; set; }
        #endregion

        #region Constructors
        public TUserRole()
        {
        }
        public TUserRole(String userName, ERole roleId)
        {
            UserName = userName;
            RoleId = roleId;
        }
        public TUserRole(IDataReader reader) : base(reader)
        {
        }
        #endregion

        #region Methods
        public override void Init(IDataReader reader)
        {
            this.UserName = Convert.ToString(reader[nameof(this.UserName)]);
            this.RoleId = (ERole)Enum.Parse(typeof(ERole), reader[nameof(this.RoleId)].ToString());
        }
        public override void Init(IDataReader reader, List<string> fields)
        {
            if (fields.Contains(nameof(UserName)))
            {
                this.UserName = Convert.ToString(reader[nameof(this.UserName)]);
            }
            if (fields.Contains(nameof(RoleId)))
            {
                this.RoleId = (ERole)Enum.Parse(typeof(ERole), reader[nameof(this.RoleId)].ToString());
            }
        }
        [DataMember]
        public override string TableName
        {
            get
            {
                return nameof(TUserRole);
            }
        }
        #endregion
    }
}
