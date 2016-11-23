using System.Collections.Generic;
using VL.Common.Core.DAS;

namespace VL.User.Service.Utilities
{
    public class DbConfigOfUser : DbConfigEntity
    {
        public const string DbName = nameof(VL.User);

        public DbConfigOfUser(string fileName) : base(fileName)
        {
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
