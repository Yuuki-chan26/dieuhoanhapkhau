﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Web.Mvc.Html;
using System.Linq.Expressions;
namespace idocNet.Core.Web.Mvc.Grid
{
    public static class GridExtensions
    {
        public static IHtmlString GridFor(this HtmlHelper html, Type modelType, IEnumerable dataSource)
        {
            return GridFor(html, modelType, dataSource, "grid");
        }
        public static IHtmlString GridFor(this HtmlHelper html, Type modelType, IEnumerable dataSource, string templateName)
        {
            return new HtmlString(html.Partial(templateName, new GridModel(modelType, dataSource)).ToString());
        }
        public static IHtmlString GridForModel<T>(this HtmlHelper<IEnumerable<T>> html)
        {
            return html.GridFor(typeof(T), html.ViewData.Model);
        }
        public static IHtmlString GridForModel<T>(this HtmlHelper<IEnumerable<T>> html, string templateName)
        {
            return html.GridFor(typeof(T), html.ViewData.Model, templateName);
        }
    }
}
