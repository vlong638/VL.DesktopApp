using System;
using System.Collections.Generic;
using VL.Common.ORM;

namespace VL.User.Objects.Entities
{
    public partial class TUserBasicInfo : IPDMTBase
    {
        public TUser User { get; set; }
    }
}
