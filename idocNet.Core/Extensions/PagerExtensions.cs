using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Text;

namespace idocNet.Core.Extensions
{
    public static class RouteExtensions
    {
        public static string GetVirtualPath(this RouteBase route, object values)
        {
            return null;
        }
    }

    public static class PagerExtensions
    {
        #region HtmlHelper extensions
        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, IPagedList list)
        {
            return Pager(htmlHelper, list, null, null);
        }

        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, IPagedList list, string labelPrevious, string labelNext)
        {
            return Pager(htmlHelper, list.PageSize, list.PageIndex, list.TotalItemCount, labelPrevious, labelNext);
        }

        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string labelPrevious, string labelNext)
        {
            return Pager(htmlHelper, pageSize, currentPage, totalItemCount, null, null, labelPrevious, labelNext);
        }

        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string actionName, string labelPrevious, string labelNext)
        {
            return Pager(htmlHelper, pageSize, currentPage, totalItemCount, actionName, null, labelPrevious, labelNext);
        }

        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, object values, string labelPrevious, string labelNext, string labelFirst, string labelLast)
        {
            return Pager(htmlHelper, pageSize, currentPage, totalItemCount, null, new RouteValueDictionary(values), labelPrevious, labelNext, labelFirst, labelLast);
        }

        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string actionName, object values, string labelPrevious, string labelNext)
        {
            return Pager(htmlHelper, pageSize, currentPage, totalItemCount, actionName, new RouteValueDictionary(values), labelPrevious, labelNext);
        }

        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, RouteValueDictionary valuesDictionary, string labelPrevious, string labelNext)
        {
            return Pager(htmlHelper, pageSize, currentPage, totalItemCount, null, valuesDictionary, labelPrevious, labelNext);
        }

        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string actionName, RouteValueDictionary valuesDictionary, string labelPrevious, string labelNext, string labelFirst, string labelLast)
        {
            if (valuesDictionary == null)
            {
                valuesDictionary = new RouteValueDictionary();
            }
            if (actionName != null)
            {
                if (valuesDictionary.ContainsKey("action"))
                {
                    throw new ArgumentException("The valuesDictionary already contains an action.", "actionName");
                }
                valuesDictionary.Add("action", actionName);
            }
            var pager = new Pager(htmlHelper, pageSize, currentPage, totalItemCount, valuesDictionary, labelPrevious, labelNext, labelFirst, labelLast);
            return pager.RenderHtml();
        }

        public static MvcHtmlString Pager1(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string actionName, RouteValueDictionary valuesDictionary, string labelPrevious, string labelNext, string labelFirst, string labelLast)
        {
            if (valuesDictionary == null)
            {
                valuesDictionary = new RouteValueDictionary();
            }
            if (actionName != null)
            {
                if (valuesDictionary.ContainsKey("action"))
                {
                    throw new ArgumentException("The valuesDictionary already contains an action.", "actionName");
                }
                valuesDictionary.Add("action", actionName);
            }
            var pager = new Pager(htmlHelper, pageSize, currentPage, totalItemCount, valuesDictionary, labelPrevious, labelNext, labelFirst, labelLast);
            return pager.RenderHtml1();
        }
        public static MvcHtmlString Pager1(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, object values, string labelPrevious, string labelNext, string labelFirst, string labelLast)
        {
            return Pager1(htmlHelper, pageSize, currentPage, totalItemCount, null, new RouteValueDictionary(values), labelPrevious, labelNext, labelFirst, labelLast);
        }

        #endregion
    }


    public class Pager
    {
        public string LabelNext
        {
            get;
            set;
        }

        public string LabelPrevious
        {
            get;
            set;
        }

        public string LabelFirst
        {
            get;
            set;
        }

        public string LabelLast
        {
            get;
            set;
        }

        private HtmlHelper _htmlHelper;
        private readonly int pageSize;
        private readonly int currentPage;
        private readonly int totalItemCount;
        private readonly RouteValueDictionary linkWithoutPageValuesDictionary;

        public Pager(HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, RouteValueDictionary valuesDictionary, string labelPrevious, string labelNext, string labelFirst, string labelLast)
            : this()
        {
            this._htmlHelper = htmlHelper;
            this.pageSize = pageSize;
            this.currentPage = currentPage;
            this.totalItemCount = totalItemCount;
            this.linkWithoutPageValuesDictionary = valuesDictionary;

            if (labelPrevious != null)
            {
                this.LabelPrevious = labelPrevious;
            }
            if (labelNext != null)
            {
                this.LabelNext = labelNext;
            }
            if (labelFirst != null)
            {
                this.LabelFirst = labelFirst;
            }
            if (labelLast != null)
            {
                this.LabelLast = labelLast;
            }
        }

        public Pager()
        {
            this.LabelNext = "Next >>";
            this.LabelPrevious = "<< Previous";
            this.LabelFirst = "First";
            this.LabelLast = "Last";
        }

        public MvcHtmlString RenderHtml()
        {
            string culture = (string)this._htmlHelper.ViewContext.RouteData.Values["CultureName"];

            int pageCount = (int)Math.Ceiling(this.totalItemCount / (double)this.pageSize);
            if (pageCount <= 1)
            {
                return null;
            }
            int nrOfPagesToDisplay = 10;

            var sb = new StringBuilder();

            // Previous
            if (this.currentPage > 0)
            {
                sb.Append(GeneratePageLink(this.LabelFirst, culture, 0, new { @class = "previous", rel = "prev" }));
                sb.Append(GeneratePageLink(this.LabelPrevious, culture, this.currentPage - 1, new { @class = "previous", rel = "prev" }));
            }
            else
            {
                sb.Append("<span class=\"disabled previous\">" + _htmlHelper.Encode(this.LabelFirst) + "</span> &nbsp;");
                sb.Append("<span class=\"disabled previous\">" + _htmlHelper.Encode(this.LabelPrevious) + "</span>");
            }

            int start = 0;
            int end = pageCount;

            if (pageCount > nrOfPagesToDisplay)
            {
                int middle = (int)Math.Ceiling(nrOfPagesToDisplay / 2d) - 1;
                int below = (this.currentPage - middle);
                int above = (this.currentPage + middle);

                if (below < 4)
                {
                    above = nrOfPagesToDisplay;
                    below = 0;
                }
                else if (above > (pageCount - 4))
                {
                    above = pageCount;
                    below = (pageCount - nrOfPagesToDisplay);
                }

                start = below;
                end = above;
            }

            if (start > 3)
            {
                sb.Append(GeneratePageLink("1", culture, 0, null));
                sb.Append(GeneratePageLink("2", culture, 1, null));
                sb.Append("<span class=\"more\">...</span>");
            }
            for (int i = start; i < end; i++)
            {
                if (i == this.currentPage)
                {
                    sb.AppendFormat("<span class=\"current\">{0}</span>", i + 1);
                }
                else
                {
                    sb.Append(GeneratePageLink((i + 1).ToString(), culture, i, null));
                }
            }
            if (end < (pageCount - 3))
            {
                sb.Append("<span class=\"more\">...</span>");
                sb.Append(GeneratePageLink((pageCount - 1).ToString(), culture, pageCount - 2, null));
                sb.Append(GeneratePageLink(pageCount.ToString(), culture, pageCount - 1, null));
            }

            // Next
            if (this.currentPage < pageCount - 1)
            {
                //this._htmlHelper.ActionLinkFormatted(viewContext.RouteData.Values["controller"]
                sb.Append(GeneratePageLink(this.LabelNext, culture, (this.currentPage + 1), new { @class = "next", rel = "next" }));
                sb.Append(GeneratePageLink(this.LabelLast, culture, (pageCount - 1), new { @class = "next", rel = "next" }));
            }
            else
            {
                sb.Append("<span class=\"disabled next\">" + _htmlHelper.Encode(this.LabelNext) + "</span>");
                sb.Append("<span class=\"disabled next\">" + _htmlHelper.Encode(this.LabelLast) + "</span>");
            }
            sb.Insert(0, "<div class=\"pager\">");
            sb.Append("</div>");

            return MvcHtmlString.Create(sb.ToString());
        }
        public MvcHtmlString RenderHtml1()
        {
            string culture = (string)this._htmlHelper.ViewContext.RouteData.Values["CultureName"];

            int pageCount = (int)Math.Ceiling(this.totalItemCount / (double)this.pageSize);
            if (pageCount <= 1)
            {
                return null;
            }
            int nrOfPagesToDisplay = 10;

            var sb = new StringBuilder();

            // Previous
            //if (this.currentPage > 0)
            //{
            //    sb.Append(GeneratePageLink(this.LabelFirst, culture, 0, new { @class = "previous", rel = "prev" }));
            //    sb.Append(GeneratePageLink(this.LabelPrevious, culture, this.currentPage - 1, new { @class = "previous", rel = "prev" }));
            //}
            //else
            //{
            //    sb.Append("<span class=\"disabled previous\">" + _htmlHelper.Encode(this.LabelFirst) + "</span> &nbsp;");
            //    sb.Append("<span class=\"disabled previous\">" + _htmlHelper.Encode(this.LabelPrevious) + "</span>");
            //}

            int start = 0;
            int end = pageCount;

            if (pageCount > nrOfPagesToDisplay)
            {
                int middle = (int)Math.Ceiling(nrOfPagesToDisplay / 2d) - 1;
                int below = (this.currentPage - middle);
                int above = (this.currentPage + middle);

                if (below < 4)
                {
                    above = nrOfPagesToDisplay;
                    below = 0;
                }
                else if (above > (pageCount - 4))
                {
                    above = pageCount;
                    below = (pageCount - nrOfPagesToDisplay);
                }

                start = below;
                end = above;
            }
            sb.Append("<ul class=\"pagination\">");
            // Previous
            if (this.currentPage > 0)
            {
                sb.Append("<li>" + GeneratePageLink("«", culture, (this.currentPage - 1), new { @class = "next", rel = "next" }) + "</li>");
            }
            else
            {
                sb.Append("<li class=\"disabled\"><a href=\"#\">«</a></li>");
            }
            if (start > 3)
            {
                sb.Append("<li>" + GeneratePageLink("1", culture, 0, null) + "</li>");
                sb.Append("<li>" + GeneratePageLink("2", culture, 1, null) + "</li>");
                sb.Append("<li class=\"disabled\"><a href=\"#\">...</a></li>");
            }
            for (int i = start; i < end; i++)
            {
                if (i == this.currentPage)
                {
                    sb.AppendFormat("<li class=\"active\"><a href=\"#\">{0}</a></li>", i + 1);
                }
                else
                {
                    sb.Append("<li>" + GeneratePageLink((i + 1).ToString(), culture, i, null) + "</li>");
                }
            }
            if (end < (pageCount - 3))
            {
                sb.Append("<li class=\"disabled\"><a href=\"#\">...</a></li>");
                sb.Append("<li>" + GeneratePageLink((pageCount - 1).ToString(), culture, pageCount - 2, null) + "</li>");
                sb.Append("<li>" + GeneratePageLink(pageCount.ToString(), culture, pageCount - 1, null) + "</li>");
            }

            // Next
            if (this.currentPage < pageCount - 1)
            {
                //this._htmlHelper.ActionLinkFormatted(viewContext.RouteData.Values["controller"]
                sb.Append("<li>" + GeneratePageLink("»", culture, (this.currentPage + 1), new { @class = "next", rel = "next" }) + "</li>");
            }
            else
            {
                sb.Append("<li class=\"disabled\"><a href=\"#\">»</a></li>");
            }
            sb.Append("</ul>");
            return MvcHtmlString.Create(sb.ToString());
        }

        private MvcHtmlString GeneratePageLink(string linkText, string culture, int pageIndex, object htmlAttributes)
        {
            RouteValueDictionary values = new RouteValueDictionary(this._htmlHelper.ViewContext.RouteData.Values);
            values.Remove("action");
            values.Remove("controller");
            values["culturename"] = culture;
            values["page"] = pageIndex;
            foreach (KeyValuePair<string, object> keyPair in this.linkWithoutPageValuesDictionary)
            {
                if (!values.ContainsKey(keyPair.Key))
                {
                    values.Add(keyPair.Key, keyPair.Value);
                }
            }

            return this._htmlHelper.ActionLink(linkText, this._htmlHelper.ViewContext.RouteData.Values["action"].ToString(), this._htmlHelper.ViewContext.RouteData.Values["controller"].ToString(), values, new RouteValueDictionary(htmlAttributes));
        }
    }
}
