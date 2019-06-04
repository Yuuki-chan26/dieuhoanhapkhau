using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace idocNet.Core.Web.Mvc.Grid
{
    public interface IItemColumnRender
    {
        IHtmlString Render(object dataItem, object value, ViewContext viewContext);
    }
}
