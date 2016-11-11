using System;
using System.Collections.Generic;
using System.Xml.Linq;
using VL.Common.DAS;

namespace VL.User.Service.Configs
{
    public class DbConfigOfUser : DbConfigEntity
    {
        public const string DbName = nameof(VL.User);

        public DbConfigOfUser(string fileName) : base(fileName)
        {
        }

        public override IEnumerable<XElement> ToXElements()
        {
            throw new NotImplementedException();
        }

        protected override List<DbConfigItem> GetDbConfigItems()
        {
            List<DbConfigItem> result = new List<DbConfigItem>()
            {
                new DbConfigItem(nameof(VL.User)),
            };
            return result;
        }
    }
}
