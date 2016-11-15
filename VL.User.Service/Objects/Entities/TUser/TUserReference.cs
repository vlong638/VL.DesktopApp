using System;
using System.Collections.Generic;
using VL.Common.ORM;

namespace VL.User.Objects.Entities
{
    public partial class TUser : IPDMTBase
    {
        public TUserBasicInfo UserBasicInfo { get; set; }
        public List<TUserRole> UserRoles { get; set; }
    }
}
