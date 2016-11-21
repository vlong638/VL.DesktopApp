﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;
using VL.Common.DAS;

namespace VL.User.Service.Utilities
{
    public class DbConfigOfBlog : DbConfigEntity
    {
        public const string DbName = nameof(VL.Blog);

        public DbConfigOfBlog(string fileName) : base(fileName)
        {
        }

        protected override List<DbConfigItem> GetDbConfigItems()
        {
            List<DbConfigItem> result = new List<DbConfigItem>()
            {
                new DbConfigItem(nameof(VL.Blog)),
            };
            return result;
        }
    }
}