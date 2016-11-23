using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace System.Web.Mvc
{
    public static class HtmlHelperEx
    {
        public static IHtmlString RawText(this HtmlHelper helper, string text)
        {
            return new HtmlString(text.Replace("\r\n", "<br>"));
        }
    }
}