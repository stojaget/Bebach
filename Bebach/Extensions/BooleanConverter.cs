using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class BooleanConverter
    {
        public static MvcHtmlString YesNo(this HtmlHelper htmlHelper, bool? yesNo)
        {
            var text = "";
            if (yesNo != null)
            {
                 text = (bool)yesNo ? "DA" : "NE";
                return new MvcHtmlString(text);
            }
            return new MvcHtmlString("NEPOZNATO");

        }
    }
}