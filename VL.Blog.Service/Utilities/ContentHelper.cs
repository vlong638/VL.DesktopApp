using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VL.Blog.Service.Utilities
{
    class ContentHelper
    {
        public static string GetBreviaryContent(string content)
        {
            if (content.Length>100)
            {
                return content.Substring(0, 100) + "...";
            }
            else
            {
                return content;
            }
        }
    }
}
